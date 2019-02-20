using System;
using System.Windows.Forms;

namespace NetChat.Front {
    public partial class Optionen : Form
    {
        public Optionen()
        {
            InitializeComponent();
        }

        private void UserNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (UserNameTextBox.Text.Contains("\n")) {
                UserNameTextBox.Text = UserNameTextBox.Text.Replace('\n', ' ');
            }
            GlobalVariable.Username = UserNameTextBox.Text.Replace("\n", "");
        }

        private void Optionen_Load(object sender, EventArgs e)
        {
            UserNameTextBox.Text = GlobalVariable.Username;
            pwTextBox.Text = GlobalVariable.Pw;
            PortTextBox.Text = GlobalVariable.Port.ToString();
            IPTextBox.Text = GlobalVariable.Ip;
        }

        private void PwTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GlobalVariable.Pw = pwTextBox.Text;
            }
        }

        private void IPTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter) return;
            GlobalVariable.Ip = IPTextBox.Text;
        }

        private void PortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            Logger.Debug(PortTextBox.Text);
            GlobalVariable.Port = int.Parse(PortTextBox.Text);
        }

        private void PortTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
            e.Handled = true;
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || (e.KeyChar == '.')) return;
            e.Handled = true;
        }

        private void Optionen_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.Username = UserNameTextBox.Text;
            GlobalVariable.Pw = pwTextBox.Text;
            GlobalVariable.Port = int.Parse(PortTextBox.Text);
            GlobalVariable.Ip = IPTextBox.Text;
            GlobalVariable.SafeToTemp();
            SafeInfo.Visible = true;
        }
    }
}
