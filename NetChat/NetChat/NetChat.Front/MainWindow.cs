using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                ShowMessage(GlobalVariable.UserName, "https://cdn.vox-cdn.com/thumbor/A7fzC1r2telq4fciUlmyoR5THRs=/0x0:1020x712/1200x800/filters:focal(429x275:591x437)/cdn.vox-cdn.com/uploads/chorus_image/image/61154181/lacie-2big-nas-press1_1020.1419968621.0.jpg");
            }
        }

        public void ShowMessage(String user, String msg)
        {
            if (msg.EndsWith("jpg"))
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(msg);
                Bitmap bitmap;
                bitmap = new Bitmap(stream);
                Chat.Items.Add(bitmap);
            }
            Chat.Items.Add($"[{user}] {msg}");

            Chat.SelectedIndex = Chat.Items.Count - 1;
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVariable.SafeToTemp();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            GlobalVariable.LoadFromTemp();
        }
        
        private void Senden_Click(object sender, EventArgs e) {
            
        }
    }
}
