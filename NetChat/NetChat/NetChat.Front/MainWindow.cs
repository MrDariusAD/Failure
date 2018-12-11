using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetChat.Client.Core;

namespace NetChat.Front
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private NetChatConnection _connection;
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Optionen", new EventHandler(Options));
            cm.MenuItems.Add("Verbindung herstellen", new EventHandler(InitConnection));
            cm.Show(this, new Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 20));
        }

        private void InitConnection(object sender, EventArgs e)
        {
            _connection = new NetChatConnection(GlobalVariable.IP, GlobalVariable.Port, GlobalVariable.UserName);
        }

        private void Options(object sender, EventArgs e)
        {
            Optionen o = new Optionen();
            o.ShowDialog();
        }

        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Send(ChatTextBox.Text);
            }
        }

        private void Send(string text)
        {
            if (_connection == null) {
                MessageBox.Show("Bitte zuerst Verbindung herstellen");
                return;
            }
            Console.WriteLine($@"[{GlobalVariable.UserName}]: {text}");
            _connection.SendNudes(text);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.SafeToTemp();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GlobalVariable.LoadFromTemp();
        }
        
        private void Senden_Click(object sender, EventArgs e) {
            Send(ChatTextBox.Text);
        }
    }
}
