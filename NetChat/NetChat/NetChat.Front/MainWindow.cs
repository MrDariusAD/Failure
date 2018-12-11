using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetChat.

namespace NetChat.Front
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Optionen", new EventHandler(options));
                cm.MenuItems.Add("Verbindung herstellen", new EventHandler(InitConnection));
                cm.Show(this, new Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 20));
            }
        }

        private void InitConnection(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void options(object sender, EventArgs e)
        {
            Optionen o = new Optionen();
            o.ShowDialog();
        }

        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                send(ChatTextBox.Text);
            }
        }

        private void send(string text)
        {
            Console.WriteLine("[" + GlobalVariable.UserName + "] " + text);

        }

        private void Senden_Click(object sender, EventArgs e) {
            
        }
    }
}
