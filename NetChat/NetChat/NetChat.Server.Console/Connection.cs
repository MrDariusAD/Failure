using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetChat.Client.Core;

namespace NetChat.Server.Console {
    public class Connection
    {
        public Connection(Socket socket) {
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            Thread = new Thread(ProcessMessages);
            Username = "#Unknown";
            RecievedMessages = new List<Message>();
        }

        public Socket Socket { get; set; }
        public Thread Thread { get; set; }
        public string Username { get; set; }
        public List<Message> RecievedMessages { get; set; }

        public void ProcessMessages() {
            while (true) {
                if (Socket.Available == 0) continue;
                byte[] readBytes = { };
                Socket.Receive(readBytes);
                var received = Encoding.ASCII.GetString(readBytes);
                var receivedMessage = new Message(received);
                RecievedMessages.Add(receivedMessage);
                if (receivedMessage.Username != Username) Username = receivedMessage.Username;
                System.Console.WriteLine($"Neue Nachricht erhalten: {received}");
            }
        }

        public void StartThread() {
            Thread = new Thread(ProcessMessages);
            Thread.Start();
        }

        public void StopThread() {
            Thread.Abort();
        }


    }
}
