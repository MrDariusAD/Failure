﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetChat.Front
{
    public partial class Optionen : Form
    {
        public Optionen()
        {
            InitializeComponent();
        }

        private void AllowSelfHost_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariable.allowSelfHost = AllowSelfHost.Checked;
        }

        private void UserNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                GlobalVariable.UserName = UserNameTextBox.Text;
            }
        }

        private void Optionen_Load(object sender, EventArgs e)
        {
            UserNameTextBox.Text = GlobalVariable.UserName;
            AllowSelfHost.Checked = GlobalVariable.allowSelfHost;
            pwTextBox.Text = GlobalVariable.PW;
            PortTextBox.Text = GlobalVariable.Port.ToString();
            IPTextBox.Text = GlobalVariable.IP;
        }

        private void pwTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalVariable.PW = pwTextBox.Text;
            }
        }

        private void IPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalVariable.IP = IPTextBox.Text;
            }
        }

        private void PortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalVariable.Port = int.Parse(IPTextBox.Text);
            }
        }

        private void PortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Optionen_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.UserName = UserNameTextBox.Text;
            GlobalVariable.allowSelfHost = AllowSelfHost.Checked;
            GlobalVariable.PW = pwTextBox.Text;
            GlobalVariable.Port = int.Parse(PortTextBox.Text); 
            GlobalVariable.IP = IPTextBox.Text;
        }
    }
}