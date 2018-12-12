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
            ShowMessage("ICH", "HELLO WORLD");
            if (e.Button != MouseButtons.Right) return;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Optionen", new EventHandler(Options));
            cm.MenuItems.Add("Verbindung herstellen", new EventHandler(InitConnection));
            cm.MenuItems.Add("Server erstellen", new EventHandler(InitServer));
            cm.Show(this, new Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 20));
        }

        private void InitServer(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitConnection(object sender, EventArgs e)
        {
            _connection = new NetChatConnection(GlobalVariable.IP, GlobalVariable.Port, GlobalVariable.UserName);
            _connection.
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
            ShowMessage(GlobalVariable.UserName, text);
            _connection.SendNudes(text);
        }

        public void ShowMessage(String user, String msg)
        {
            Chat.Items.Add($"[{user}] {msg}");
            Chat.SelectedIndex = Chat.Items.Count - 1;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.SafeToTemp();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GlobalVariable.LoadFromTemp();
            ResizeWindow();
        }
        
        private void Senden_Click(object sender, EventArgs e) {
            Send(ChatTextBox.Text);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            ResizeWindow();
        }

        public void ResizeWindow()
        {
            int x = this.Width,
               y = this.Height;
            OuterBox.Width = x - 60;
            OuterBox.Height = y - 80;
            OuterBox.Top = 22;
            OuterBox.Left = 22;
            x = OuterBox.Width;
            y = OuterBox.Height;
            Chat.Width = x - 40;
            Chat.Height = y - 50;
            Chat.Left = 20;
            Chat.Top = 20;
            ChatTextBox.Width = x - 130;
            ChatTextBox.Height = 20;
            Senden.Height = 20;
            Senden.Width = 80;
            Senden.Left = x - 100;
            ChatTextBox.Top = y - 27;
            Senden.Top = ChatTextBox.Top;
        }
    }
}
