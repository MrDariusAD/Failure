using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NetChat.Client.Core
{
    public class NetChatConnection
    {
        public string ConnectionUrl { get; }
        public int ConnectionPort { get; }
        public string Username { get; }
        public List<Message> RecievedMessages;
        private NetChatSocket _socket;
        private Thread updateMessages;
        public bool ContinueWaiting = true;

        public NetChatConnection(string connectionUrl, int connectionPort, string username)
        {
            RecievedMessages = new List<Message>();
            ConnectionUrl = connectionUrl;
            ConnectionPort = connectionPort;
            Username = username;

            _socket = new NetChatSocket(ConnectionUrl, connectionPort);
            if (!_socket.IsInit)
            {
                _socket = null;
            }
            else
            {
                updateMessages = new Thread(ChatUpdater);
                updateMessages.Start();
            }
        }
        public void SendNudes(string messageString)
        {
            if (!_socket.IsInit) throw new Exception("Uninitialized Socket");
            var message = new Message(messageString, false, Username);
            _socket.SendMessage(message);
        }

        public void Destroy()
        {
            _socket.DestroyConnection();
        }

        public void ChatUpdater()
        {
            if (!_socket.IsInit)
                return;
            while (ContinueWaiting)
            {
                try
                {
                    if (_socket._socket.Available == 0)
                        continue;
                }
                catch (ObjectDisposedException ex)
                {
                    Destroy();
                    break;
                }
                byte[] readBytes = new byte[_socket._socket.Available];
                int size = _socket._socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                System.Console.WriteLine("Client - Received Raw: " + received);
                Message receivedMessage = new Message(received);
                RecievedMessages.Add(receivedMessage);
            }
        }


        public bool SocketIsNull()
        {
            if (_socket == null)
                return true;
            return false;
        }
    }
}
