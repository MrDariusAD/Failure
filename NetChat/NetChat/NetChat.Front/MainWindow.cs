using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NetChat.Client.Core;
using NetChat.Server.Console;
using Message = NetChat.Client.Core.Message;

namespace NetChat.Front {
    public partial class MainWindow : Form
    {
        private NetChatServer _server;
        private ClientConnection _connection;
        private Thread _chatUpdater;
        private bool _keepUpdating = true;
        private bool _stillSending;
        private bool fokus = true;

        #region Konstructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region Events
        private void MainWindow_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Fokus: True");
            fokus = true;
            ChatTextBox.Focus();
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var cm = new ContextMenu();
            cm.MenuItems.Add("Optionen", Options);
            if (IsConnectionDead())
                cm.MenuItems.Add("Verbindung herstellen", InitConnection);
            else
                cm.MenuItems.Add("Verbindung trennen", DestroyConnection);
            if (_server == null || !_server.IsRunning())
                cm.MenuItems.Add("Server erstellen", InitServer);
            else
                cm.MenuItems.Add("Server beenden", EndServer);
            cm.Show(this, new Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 30));
        }

        private void EndServer(object sender, EventArgs e)
        {
            DestroyServer();
        }

        private void DestroyConnection(object sender, EventArgs e)
        {
            _keepUpdating = false;
            _connection.Destroy();
            _connection = null;
        }

        private void InitServer(object sender, EventArgs e)
        {
            ShowMessage("INFO", "Der Server wird gestartet");
            _server = new NetChatServer(GlobalVariable.Port, GlobalVariable.Pw);
            _server.StartServer();
            ShowMessage("INFO", "Der Server wurde gestartet - IP: " + GlobalVariable.Ip + ":" + GlobalVariable.Port);
        }

        private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send(ChatTextBox.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientConnection.ContinueWaiting = false;
            _keepUpdating = false;
            GlobalVariable.SafeToTemp();
            if (_server != null)
                DestroyServer();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GlobalVariable.LoadFromTemp();
            ResizeWindow();
        }

        private void Senden_Click(object sender, EventArgs e)
        {
            Send(ChatTextBox.Text);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            ResizeWindow();
        }

        private void Options(object sender, EventArgs e)
        {
            var o = new Optionen();
            o.ShowDialog();
        }

        private void InitConnection(object sender, EventArgs e)
        {
            ShowMessage("INFO", $"Verbinde zu {GlobalVariable.Ip}:{GlobalVariable.Port} - With the Username: {GlobalVariable.Username}");
            _connection = new ClientConnection(GlobalVariable.Ip, GlobalVariable.Port, GlobalVariable.Username, GlobalVariable.Pw);
            if (!_connection.SocketIsNull()) {
                if (!_connection.IsInit()) return;
                _chatUpdater = new Thread(Updater);
                _chatUpdater.Start();
                ShowMessage("INFO", "Eine Verbindung wurde hergestellt");
            }
            else {
                _connection = null;
            }
        }
        #endregion
        #region Methoden
        private bool IsCommand(string text)
        {
            if (text[0] == '/')
                return true;
            return false;
        }

        public void ShowMessage(string user, string msg)
        {
            Notifiy(user, msg);
            Logger.Info($"[{user}] {msg}");
            Chat.Items.Add($"[{user}] {msg}");
            Chat.SelectedIndex = Chat.Items.Count - 1;
        }

        private void DestroyServer()
        {
            _server.Destroy();
            _server = null;
        }

        private bool IsConnectionDead()
        {
            return _connection == null || !_connection.IsInit() || _connection.SocketIsNull();
        }

        public void ResizeWindow()
        {
            int x = Width,
               y = Height;
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

        #region Update from Outer
        private delegate void delUpdateListBox(String user, String text);

        private void UpdateListBox(String user, String msg)
        {
            ShowMessage(user, msg);
        }

        private void Updater()
        {
            _keepUpdating = true;
            while (_keepUpdating && !IsConnectionDead())
            {
                Thread.CurrentThread.Join(20);
                if (_connection == null)
                    break;
                foreach (Message m in _connection?.RecievedMessages.Where(x => x != null).ToList())
                {
                    delUpdateListBox delUpdate = UpdateListBox;
                    Chat.BeginInvoke(delUpdate, m.Username, m.Content);
                }
                _connection?.RecievedMessages.Clear();
            }
        }
        #endregion
        private void Send(string text)
        {
            if (text.Length == 0)
                return;
            if (text[0] == '/')
                if (TryHandleLocal(text))
                {
                    ChatTextBox.Text = "";
                    return;
                }
            if (text.Contains("\n"))
            {
                text.Replace("\n", "");
            }

            if (_connection == null)
            {
                MessageBox.Show(@"Bitte zuerst Verbindung herstellen", @"Keine Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_stillSending)
                return;
            _stillSending = true;
            ChatTextBox.Clear();
            var m = new Message(text, IsCommand(text), GlobalVariable.Username);
            if (!_connection.SendNudes(m))
            {
                if (!_connection.IsInit())
                    _connection.Destroy();
                _connection = null;
                ShowMessage("INFO", "Der Server ist nicht mehr erreichbar. Die Verbindung wurde beendet");
            }
            _stillSending = false;
        }

        private bool TryHandleLocal(string text)
        {
            text = text.ToLower();
            switch (text)
            {
                case "/clear":
                    Chat.Items.Clear();
                    break;
                case "/blyat":
                    DoBlyat();
                    break;
                case "/help":
                    ShowMessage(GlobalVariable.Username, "/help");
                    ShowMessage("HELP", "/clear - Löscht alle Nachrichten");
                    ShowMessage("HELP", "/kill - Beendet den Server");
                    ShowMessage("HELP", "/msg - Schickt eine Nachricht seitens des Servers");
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void Notifiy(String sender, String msg)
        {
            if (!fokus)
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Icon = SystemIcons.Application;
                notifyIcon.BalloonTipTitle = sender;
                notifyIcon.BalloonTipText = msg;
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(30000);
            }
        }

        private void DoBlyat()
        {
            Senden.Text = @"сука блять";
            Text = @"Йíэт Chaт";
            OuterBox.BackColor = Color.Transparent;
            try
            {
                Console.WriteLine(@"DOING BLYAT! CYKA");
                Image img = Image.FromFile("flag.png");
                BackgroundImage = img;
                //OuterBox.BackgroundImage = img;
                BackColor = Color.Black;
                Console.WriteLine(@"DID BLYAT! CYKA");
            }
            catch (Exception) {
                // ignored
            }
        }
        #endregion

        private void MainWindow_Deactivate(object sender, EventArgs e)
        {
            Console.WriteLine("Fokus: False");
            fokus = false;
        }
    }
}
