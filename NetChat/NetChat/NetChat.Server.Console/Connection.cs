using NetChat.Client.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetChat.Server.Console {
    public class Connection
    {

        public NetChatServerSocket ServerSocket { get; }
        public Socket Socket { get; set; }
        public Thread Thread { get; set; }
        public string Username { get; set; }
        public List<Message> RecievedMessages { get; set; }

        public Connection(Socket socket, NetChatServerSocket ServerSocket)
        {
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
                } catch(ObjectDisposedException ex)
                {
                    Close();
                    break;
                }
                byte[] readBytes = new byte[Socket.Available];
                int size = Socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                Message receivedMessage = new Message(received);
                RecievedMessages.Add(receivedMessage);
                ServerSocket.SendToOthers(receivedMessage);
                if (receivedMessage.Username != Username) Username = receivedMessage.Username;
                System.Console.WriteLine($"Server - Neue Nachricht erhalten: {received}");
            }
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
