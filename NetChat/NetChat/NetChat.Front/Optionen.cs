using System;
using System.Windows.Forms;

namespace NetChat.Front
{
    public partial class Optionen : Form
    {
        public Optionen()
        {
            InitializeComponent();
        }

        private void UserNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalVariable.USERNAME = UserNameTextBox.Text;
            }
        }

        private void Optionen_Load(object sender, EventArgs e)
        {
            UserNameTextBox.Text = GlobalVariable.USERNAME;
            pwTextBox.Text = GlobalVariable.PW;
            PortTextBox.Text = GlobalVariable.PORT.ToString();
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
                Logger.Debug(PortTextBox.Text);
                GlobalVariable.PORT = int.Parse(PortTextBox.Text);
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
            GlobalVariable.USERNAME = UserNameTextBox.Text;
            GlobalVariable.PW = pwTextBox.Text;
            GlobalVariable.PORT = int.Parse(PortTextBox.Text);
            GlobalVariable.IP = IPTextBox.Text;
            GlobalVariable.SafeToTemp();
        }
    }
}
