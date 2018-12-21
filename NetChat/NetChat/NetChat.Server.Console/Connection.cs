using NetChat.Client.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetChat.Server.Console
{
    public class Connection
    {
        private string Pw;

        private NetChatServerSocket ServerSocket { get; }
        public Socket Socket { get; set; }
        public Thread Thread { get; set; }
        private string Username { get; set; }
        private List<Message> RecievedMessages { get; set; }
        private bool validatedViaPassword = false;

        public Connection(Socket socket, NetChatServerSocket ServerSocket, String pw)
        {
            this.Pw = pw;
            this.ServerSocket = ServerSocket;
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            Thread = new Thread(ProcessMessages);
            RecievedMessages = new List<Message>();
            StartThread();
        }

        public void ProcessMessages()
        {
            while (true)
            {
                System.Threading.Thread.CurrentThread.Join(500);
                try
                {
                    if (Socket.Available == 0)
                        continue;
                }
                catch (ObjectDisposedException)
                {
                    Close();
                    break;
                }
                byte[] readBytes = new byte[Socket.Available];
                int size = Socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                System.Console.WriteLine($"Server - Neue Nachricht erhalten: {received}");
                Logger.Debug($"Server - Neue Nachricht erhalten: {received}");
                // Für den Fall das während der Verarbeitungszeit 2 Nachrichten reingekommen sind
                string[] proerties = received.Split("#\\#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string m in proerties)
                {
                    // Es wird gebrüft ob der string was enhällt weil das letzte Feld immer leer sein wird
                    if (m.Length > 1)
                        singleMessage(m);
                }
                //if (receivedMessage.Username != Username)
                //    Username = receivedMessage.Username;
            }
        }

        public void SendMessage(String message, bool command = false)
        {
            Message m = new Message(message, command, "Server");
            SendMessage(m);
        }

        public void SendMessage(Message message)
        {
            byte[] KickMSG = Encoding.ASCII.GetBytes(message.ToString());
            Socket.Send(KickMSG);
        }

        private void singleMessage(string received)
        {
            Message receivedMessage = new Message(received);
            CheckNameChange(receivedMessage);
            if (receivedMessage.IsCommand)
            {
                if (receivedMessage.Content.StartsWith("/pw:"))
                {
                    if (!IsPasswortValide(receivedMessage.Content))
                    {
                        SendMessage("Das Passwort ist falsch");
                        Close();
                    }
                    else
                    {
                        validatedViaPassword = true;
                        Message m = new Message(receivedMessage.Username + " ist den Chatroom beigetreten", false, "Server");
                        ServerSocket.SendToOthers(m);
                    }
                }
                else
                    ServerSocket.HandleCommand(receivedMessage);
            }
            else
            {
                if (!validatedViaPassword)
                {
                    SendMessage("/pwKick", true);
                    Thread.Sleep(20);
                    Close();
                }
                ServerSocket.SendToOthers(receivedMessage);
                RecievedMessages.Add(receivedMessage);
            }
        }

        private void CheckNameChange(Message receivedMessage)
        {
            if (Username == null)
            {
                Username = receivedMessage.Username;
            }
            else
            {
                if (Username != receivedMessage.Username)
                {
                    Message nameChangeMessage = new Message(Username + " hat sich zu " + receivedMessage.Username + " umbenannt", false, "Server");
                    ServerSocket.SendToOthers(nameChangeMessage);
                    Username = receivedMessage.Username;
                }

            }
        }

        private bool IsPasswortValide(string pw)
        {
            pw = pw.Substring(4);
            System.Console.WriteLine("PW: " + pw);
            if (Pw == pw)
                return true;
            return false;
        }

        public void StartThread()
        {
            Thread.Start();
        }

        public void StopThread()
        {
            Thread.Abort();
        }

        internal void Close()
        {
            Socket.Close();
        }
    }
}
