using NetChat.Client.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetChat.Server.Console {
    public class Connection
    {
        private string PW;

        public NetChatServerSocket ServerSocket { get; }
        public Socket Socket { get; set; }
        public Thread Thread { get; set; }
        public string Username { get; set; }
        public List<Message> RecievedMessages { get; set; }

        public Connection(Socket socket, NetChatServerSocket ServerSocket, String pw)
        {
            this.PW = pw;
            this.ServerSocket = ServerSocket;
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            Thread = new Thread(ProcessMessages);
            Username = "#Unknown";
            RecievedMessages = new List<Message>();
            StartThread();
        }

        public void ProcessMessages()
        {
            while (true)
            {
                try
                {
                    if (Socket.Available == 0)
                        continue;
                } catch(ObjectDisposedException)
                {
                    Close();
                    break;
                }
                byte[] readBytes = new byte[Socket.Available];
                int size = Socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                Message receivedMessage = new Message(received);
                if (receivedMessage.IsCommand)
                {
                    if (receivedMessage.Content.StartsWith("/pw:"))
                    {
                        if (!IsPasswortValide(receivedMessage.Content))
                        {
                            Message m = new Message("Das Passwort ist falsch", false, "Server");
                            byte[] messageAsBytes = Encoding.ASCII.GetBytes(m.ToString());
                            Socket.Send(messageAsBytes);
                            Close();
                        }
                    }
                    else
                        ServerSocket.HandleCommand(receivedMessage);
                }
                else
                {
                    ServerSocket.SendToOthers(receivedMessage);
                    RecievedMessages.Add(receivedMessage);
                }
                //if (receivedMessage.Username != Username)
                //    Username = receivedMessage.Username;
                System.Console.WriteLine($"Server - Neue Nachricht erhalten: {received}");
            }
        }

        private bool IsPasswortValide(string pw)
        {
            pw = pw.Substring(4);
            System.Console.WriteLine("PW: " + pw);
            if (PW == pw)
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
