// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Text;
using System.Text.RegularExpressions;
using NClass.Core;
using NClass.Translations;

namespace NClass.CSharp
{
	internal sealed class CSharpConstructor : Constructor
	{
		// [<access>] [static] <name>([<args>])
		const string ConstructorPattern =
			@"^\s*" + CSharpLanguage.AccessPattern + @"(?<static>static\s+)?" +
			@"(?<name>" + CSharpLanguage.NamePattern + ")" +
			@"\((?(static)|(?<args>.*))\)" + CSharpLanguage.DeclarationEnding;

		static Regex constructorRegex =
			new Regex(ConstructorPattern, RegexOptions.ExplicitCapture);

		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal CSharpConstructor(CompositeType parent) : base(parent)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public override string Name
		{
			get
			{
				return GetNameWithoutGeneric(Parent.Name);
			}
			set
			{
				if (value != null && value != GetNameWithoutGeneric(Parent.Name))
					throw new BadSyntaxException(Strings.ErrorConstructorName);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set access visibility.
		/// </exception>
		public override AccessModifier AccessModifier
		{
			get
			{
				return base.AccessModifier;
			}
			set
			{
				try {
					RaiseChangedEvent = false;

					if (value != AccessModifier.Default)
						IsStatic = false;
					base.AccessModifier = value;
				}
				finally {
					RaiseChangedEvent = true;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set static modifier.
		/// </exception>
		public override bool IsStatic
		{
			get
			{
				return base.IsStatic;
			}
			set
			{
				if (value && HasParameter)
					throw new BadSyntaxException(Strings.ErrorStaticConstructor);

				try {
					RaiseChangedEvent = false;

					if (value)
						AccessModifier = AccessModifier.Default;
					base.IsStatic = value;
				}
				finally {
					RaiseChangedEvent = true;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set sealed modifier.
		/// </exception>
		public override bool IsSealed
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetModifier);
			}
		}

		public override Language Language
		{
			get { return CSharpLanguage.Instance; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			Match match = constructorRegex.Match(declaration);
			RaiseChangedEvent = false;

			try {
				if (match.Success) {
					ClearModifiers();
					Group nameGroup = match.Groups["name"];
					Group accessGroup = match.Groups["access"];
					Group staticGroup = match.Groups["static"];
					Group argsGroup = match.Groups["args"];

					ValidName = nameGroup.Value;
					AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);
					ArgumentList.InitFromString(argsGroup.Value);
					IsStatic = staticGroup.Success;
				}
				else {
					throw new BadSyntaxException(Strings.ErrorInvalidDeclaration);
				}
			}
			finally {
				RaiseChangedEvent = true;
			}
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder(50);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			if (IsStatic) {
				builder.Append("static ");
			}

			builder.AppendFormat("{0}(", Name);

			for (int i = 0; i < ArgumentList.Count; i++) {
				builder.Append(ArgumentList[i]);
				if (i < ArgumentList.Count - 1)
					builder.Append(", ");
			}
			builder.Append(")");

			return builder.ToString();
		}


		public override Operation Clone(CompositeType newParent)
		{
			CSharpConstructor constructor = new CSharpConstructor(newParent);
			constructor.CopyFrom(this);
			return constructor;
		}
	}
}
