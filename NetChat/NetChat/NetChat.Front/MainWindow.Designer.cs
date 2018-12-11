namespace NetChat.Front
{
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

        #region Vom Windows Form-Designer generierter Code

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
            this.OuterBox.Location = new System.Drawing.Point(18, 18);
            this.OuterBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OuterBox.Name = "OuterBox";
            this.OuterBox.Size = new System.Drawing.Size(1164, 655);
            this.OuterBox.TabIndex = 0;
            this.OuterBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            // 
            // ChatTextBox
            // 
            this.ChatTextBox.Location = new System.Drawing.Point(30, 609);
            this.ChatTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChatTextBox.Name = "ChatTextBox";
            this.ChatTextBox.Size = new System.Drawing.Size(970, 26);
            this.ChatTextBox.TabIndex = 2;
            this.ChatTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatTextBox_KeyDown);
            // 
            // Senden
            // 
            this.Senden.Location = new System.Drawing.Point(1011, 609);
            this.Senden.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Senden.Name = "Senden";
            this.Senden.Size = new System.Drawing.Size(112, 35);
            this.Senden.TabIndex = 1;
            this.Senden.Text = "Senden";
            this.Senden.UseVisualStyleBackColor = true;
            this.Senden.Click += new System.EventHandler(this.Senden_Click);
            // 
            // Chat
            // 
            this.Chat.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Chat.FormattingEnabled = true;
            this.Chat.ItemHeight = 20;
            this.Chat.Location = new System.Drawing.Point(30, 34);
            this.Chat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(1092, 564);
            this.Chat.TabIndex = 0;
            this.Chat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.OuterBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "NetChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.OuterBox.ResumeLayout(false);
            this.OuterBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel OuterBox;
        private System.Windows.Forms.TextBox ChatTextBox;
        private System.Windows.Forms.Button Senden;
        private System.Windows.Forms.ListBox Chat;
    }
}

