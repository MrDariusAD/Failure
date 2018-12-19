using NetChat.Client.Core;
using NetChat.Server.Console;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace NetChat.Front {
    public partial class MainWindow : Form
    {
        private NetChatServer _server;
        private NetChatConnection _connection;
        private Thread ChatUpdater;
        private bool KeepUpdating = true;
        private bool stillSending;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Optionen", new EventHandler(Options));
            if(_connection == null)
                cm.MenuItems.Add("Verbindung herstellen", new EventHandler(InitConnection));
            else
                cm.MenuItems.Add("Verbindung trennen", new EventHandler(DestroyConnection));
            if (_server == null)
                cm.MenuItems.Add("Server erstellen", new EventHandler(InitServer));
            else
                cm.MenuItems.Add("Server beenden", new EventHandler(EndServer));
            cm.Show(this, new System.Drawing.Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 30));
        }

        private void EndServer(object sender, EventArgs e)
        {
            DestroyServer();
        }

        private void DestroyServer()
        {
            _server.Destroy();
            _server = null;
        }

        private void DestroyConnection(object sender, EventArgs e)
        {
            KeepUpdating = false;
            _connection.Destroy();
            _connection = null;
        }

        private void InitServer(object sender, EventArgs e)
        {
            _server = new NetChatServer(GlobalVariable.IP, GlobalVariable.Port);
            _server.StartServer();

            if (null == System.Windows.Application.Current)
            {
                new System.Windows.Application();
            }

        }

        private void InitConnection(object sender, EventArgs e)
        {
            ShowMessage("INFO", $"Verbinde zu {GlobalVariable.IP}:{GlobalVariable.Port} - With the Username: {GlobalVariable.UserName}");
            _connection = new NetChatConnection(GlobalVariable.IP, GlobalVariable.Port, GlobalVariable.UserName);
            if (_connection.SocketIsNull())
                _connection = null;
            if (_connection.IsInit())
            {
                ChatUpdater = new Thread(updater);
                ChatUpdater.Start();
                ShowMessage("INFO", "Eine Verbindung wurde hergestellt");
            }
        }

        private delegate void delUpdateListBox(String user, String text);

        private void UpdateListBox(String user, String msg)
        {
            ShowMessage(user, msg);
        }

        private void updater()
        {
            KeepUpdating = true;
            while (KeepUpdating)
            {
                Thread.Sleep(100);
                if (_connection == null)
                    break;
                foreach (Client.Core.Message m in _connection.RecievedMessages.Where(x => x != null).ToList())
                {
                    delUpdateListBox delUpdate = new delUpdateListBox(UpdateListBox);
                    this.Chat.BeginInvoke(delUpdate, new String[]{ m.Username, m.Content} );
                    //System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    //{
                    //    Chat.Items.Add($"[{m.Username}] {m.Content}");
                    //    Chat.SelectedIndex = Chat.Items.Count - 1;
                    //}));

                }
                _connection.RecievedMessages.Clear();
            }
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
                e.Handled = true;
            }
        }

        private void Send(string text)
        {
            Logger.Debug("True");
            if (text.Length == 0)
            {
                return;
            }
            if (_connection == null) {
                MessageBox.Show("Bitte zuerst Verbindung herstellen", "Keine Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (stillSending)
                return;
            stillSending = true;
            ChatTextBox.Clear();
            if(!_connection.SendNudes(text))
            {
                if (!_connection.IsInit())
                    _connection.Destroy();
                    _connection = null;
                ShowMessage("INFO", "Der Server ist nicht mehr erreichbar. Die Verbindung wurde beendet");
            }
            Logger.Debug("False");
            stillSending = false;
        }

        public void ShowMessage(String user, String msg)
        {
            Logger.Info($"[{user}] {msg}");
            Chat.Items.Add($"[{user}] {msg}");
            Chat.SelectedIndex = Chat.Items.Count - 1;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            NetChatConnection.ContinueWaiting = false;
            KeepUpdating = false;
            GlobalVariable.SafeToTemp();
            if(_server != null)
                DestroyServer();
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
