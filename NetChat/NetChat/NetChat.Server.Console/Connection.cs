using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetChat.Client.Core;

namespace NetChat.Server.Console
{
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

        bool SocketConnected()
        {
            bool part1 = Socket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (Socket.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        public void ProcessMessages()
        {
            while (true)
            {
                if (Socket.Available == 0) continue;
                byte[] readBytes = new byte[Socket.Available];
                int size = Socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                System.Console.WriteLine("Received Raw: " + received);
                Message receivedMessage = new Message(received);
                RecievedMessages.Add(receivedMessage);
                ServerSocket.SendToOthers(receivedMessage);
                if (receivedMessage.Username != Username) Username = receivedMessage.Username;
                System.Console.WriteLine($"Neue Nachricht erhalten: {received}");
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
