using System.Drawing;

namespace NetChat.Front {
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.OuterBox = new System.Windows.Forms.Panel();
            this.ChatTextBox = new System.Windows.Forms.TextBox();
            this.Senden = new System.Windows.Forms.Button();
            this.Chat = new System.Windows.Forms.ListBox();
            this.OuterBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OuterBox
            // 
            this.OuterBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.OuterBox.Controls.Add(this.ChatTextBox);
            this.OuterBox.Controls.Add(this.Senden);
            this.OuterBox.Controls.Add(this.Chat);
            this.OuterBox.Location = new System.Drawing.Point(24, 23);
            this.OuterBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OuterBox.Name = "OuterBox";
            this.OuterBox.Size = new System.Drawing.Size(1552, 819);
            this.OuterBox.TabIndex = 0;
            this.OuterBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            // 
            // ChatTextBox
            // 
            this.ChatTextBox.Location = new System.Drawing.Point(40, 762);
            this.ChatTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ChatTextBox.Name = "ChatTextBox";
            this.ChatTextBox.Size = new System.Drawing.Size(1292, 31);
            this.ChatTextBox.TabIndex = 2;
            this.ChatTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatTextBox_KeyDown);
            // 
            // Senden
            // 
            this.Senden.Location = new System.Drawing.Point(1348, 762);
            this.Senden.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Senden.Name = "Senden";
            this.Senden.Size = new System.Drawing.Size(150, 44);
            this.Senden.TabIndex = 1;
            this.Senden.Text = "Senden";
            this.Senden.UseVisualStyleBackColor = true;
            this.Senden.Click += new System.EventHandler(this.Senden_Click);
            // 
            // Chat
            // 
            this.Chat.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Chat.FormattingEnabled = true;
            this.Chat.ItemHeight = 25;
            this.Chat.Location = new System.Drawing.Point(40, 42);
            this.Chat.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(1454, 704);
            this.Chat.TabIndex = 0;
            this.Chat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1600, 865);
            this.Controls.Add(this.OuterBox);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainWindow";
            this.Text = "NetChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.OuterBox.ResumeLayout(false);
            this.OuterBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel OuterBox;
        private System.Windows.Forms.TextBox ChatTextBox;
        private System.Windows.Forms.Button Senden;
        private System.Windows.Forms.ListBox Chat;
    }
}

