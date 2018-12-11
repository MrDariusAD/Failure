﻿namespace NetChat.Front
{
    partial class Optionen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserName = new System.Windows.Forms.Label();
            this.pw = new System.Windows.Forms.Label();
            this.AllowSelfHost = new System.Windows.Forms.CheckBox();
            this.IP = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(13, 20);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(63, 13);
            this.UserName.TabIndex = 0;
            this.UserName.Text = "User Name:";
            // 
            // pw
            // 
            this.pw.AutoSize = true;
            this.pw.Location = new System.Drawing.Point(13, 60);
            this.pw.Name = "pw";
            this.pw.Size = new System.Drawing.Size(53, 13);
            this.pw.TabIndex = 1;
            this.pw.Text = "Passwort:";
            // 
            // AllowSelfHost
            // 
            this.AllowSelfHost.AutoSize = true;
            this.AllowSelfHost.Location = new System.Drawing.Point(16, 180);
            this.AllowSelfHost.Name = "AllowSelfHost";
            this.AllowSelfHost.Size = new System.Drawing.Size(161, 17);
            this.AllowSelfHost.TabIndex = 2;
            this.AllowSelfHost.Text = "Erlaube eigener Host zu sein";
            this.AllowSelfHost.UseVisualStyleBackColor = true;
            this.AllowSelfHost.CheckedChanged += new System.EventHandler(this.AllowSelfHost_CheckedChanged);
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(13, 100);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(88, 13);
            this.IP.TabIndex = 3;
            this.IP.Text = "Standart Host IP:";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(13, 140);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(29, 13);
            this.Port.TabIndex = 4;
            this.Port.Text = "Port:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(5, 415);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(277, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Value = 100;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(132, 17);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.UserNameTextBox.TabIndex = 6;
            this.UserNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserNameTextBox_KeyDown);
            // 
            // pwTextBox
            // 
            this.pwTextBox.Location = new System.Drawing.Point(132, 57);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.Size = new System.Drawing.Size(150, 20);
            this.pwTextBox.TabIndex = 7;
            this.pwTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwTextBox_KeyDown);
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(132, 97);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(150, 20);
            this.IPTextBox.TabIndex = 8;
            this.IPTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IPTextBox_KeyDown);
            this.IPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPTextBox_KeyPress);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(132, 137);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(150, 20);
            this.PortTextBox.TabIndex = 9;
            this.PortTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PortTextBox_KeyDown);
            this.PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortTextBox_KeyPress);
            // 
            // Optionen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 450);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.AllowSelfHost);
            this.Controls.Add(this.pw);
            this.Controls.Add(this.UserName);
            this.Name = "Optionen";
            this.Text = "Optionen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Optionen_FormClosing);
            this.Load += new System.EventHandler(this.Optionen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label pw;
        private System.Windows.Forms.CheckBox AllowSelfHost;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox pwTextBox;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
    }
}