using NetChat.Client.Core;
using NetChat.Server.Console;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NetChat.Front
{
    public partial class MainWindow : Form
    {
        private NetChatServer _server;
        private ClientConnection _connection;
        private Thread ChatUpdater;
        private bool KeepUpdating = true;
        private bool stillSending;

        #region Konstructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region Events
        private void MainWindow_Activated(object sender, EventArgs e)
        {
            this.ChatTextBox.Focus();
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Optionen", new EventHandler(Options));
            if (IsConnectionDead())
                cm.MenuItems.Add("Verbindung herstellen", new EventHandler(InitConnection));
            else
                cm.MenuItems.Add("Verbindung trennen", new EventHandler(DestroyConnection));
            if (_server == null || !_server.isRunning())
                cm.MenuItems.Add("Server erstellen", new EventHandler(InitServer));
            else
                cm.MenuItems.Add("Server beenden", new EventHandler(EndServer));
            cm.Show(this, new System.Drawing.Point(e.X + ((Control)sender).Left + 20, e.Y + ((Control)sender).Top + 30));
        }

        private void EndServer(object sender, EventArgs e)
        {
            DestroyServer();
        }

        private void DestroyConnection(object sender, EventArgs e)
        {
            KeepUpdating = false;
            _connection.Destroy();
            _connection = null;
        }

        private void InitServer(object sender, EventArgs e)
        {
            ShowMessage("INFO", "Der Server wird gestartet");
            _server = new NetChatServer(GlobalVariable.PORT, GlobalVariable.PW);
            _server.StartServer();
            ShowMessage("INFO", "Der Server wurde gestartet - IP: " + GlobalVariable.IP + ":" + GlobalVariable.PORT);
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
            KeepUpdating = false;
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
            Optionen o = new Optionen();
            o.ShowDialog();
        }

        private void InitConnection(object sender, EventArgs e)
        {
            ShowMessage("INFO", $"Verbinde zu {GlobalVariable.IP}:{GlobalVariable.PORT} - With the Username: {GlobalVariable.USERNAME}");
            _connection = new ClientConnection(GlobalVariable.IP, GlobalVariable.PORT, GlobalVariable.USERNAME, GlobalVariable.PW);
            if (_connection.SocketIsNull())
            {
                _connection = null;
                return;
            }
            if (_connection.IsInit())
            {
                ChatUpdater = new Thread(Updater);
                ChatUpdater.Start();
                ShowMessage("INFO", "Eine Verbindung wurde hergestellt");
            }
        }
        #endregion
        #region Methoden
        private bool IsCommand(String text)
        {
            if (text[0] == '/')
                return true;
            return false;
        }

        public void ShowMessage(String user, String msg)
        {
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

        #region Update from Outer
        private delegate void delUpdateListBox(String user, String text);

        private void UpdateListBox(String user, String msg)
        {
            ShowMessage(user, msg);
        }

        private void Updater()
        {
            KeepUpdating = true;
            while (KeepUpdating && !IsConnectionDead())
            {
                Thread.CurrentThread.Join(20);
                if (_connection == null)
                    break;
                foreach (Client.Core.Message m in _connection?.RecievedMessages.Where(x => x != null).ToList())
                {
                    delUpdateListBox delUpdate = new delUpdateListBox(UpdateListBox);
                    this.Chat.BeginInvoke(delUpdate, new String[] { m.Username, m.Content });
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
                MessageBox.Show("Bitte zuerst Verbindung herstellen", "Keine Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (stillSending)
                return;
            stillSending = true;
            ChatTextBox.Clear();
            Client.Core.Message m = new Client.Core.Message(text, IsCommand(text), GlobalVariable.USERNAME);
            if (!_connection.SendNudes(m))
            {
                if (!_connection.IsInit())
                    _connection.Destroy();
                _connection = null;
                ShowMessage("INFO", "Der Server ist nicht mehr erreichbar. Die Verbindung wurde beendet");
            }
            stillSending = false;
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
                    ShowMessage(GlobalVariable.USERNAME, "/help");
                    ShowMessage("HELP", "/clear - Löscht alle Nachrichten");
                    ShowMessage("HELP", "/kill - Beendet den Server");
                    ShowMessage("HELP", "/msg - Schickt eine Nachricht seitens des Servers");
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void DoBlyat()
        {
            Senden.Text = "сука блять";
            this.Text = "Йíэт Chaт";
            this.OuterBox.BackColor = Color.Transparent;
            try
            {
                Console.WriteLine(Path.GetDirectoryName(Application.ExecutablePath) + "/flag.png");
                Image img = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "/flag.png");
                BackgroundImage = img;
                //OuterBox.BackgroundImage = img;
                BackColor = Color.Black;
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
