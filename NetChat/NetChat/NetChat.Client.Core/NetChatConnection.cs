using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetChat.Client.Core {
    public class NetChatConnection
    {
        public string ConnectionUrl { get; }
        public int ConnectionPort { get; }
        public string Username { get; }
        public List<Message> RecievedMessages;
        private NetChatSocket _socket;
        private Thread updateMessages;
        public static bool ContinueWaiting = true;

        public NetChatConnection(string connectionUrl, int connectionPort, string username, string password)
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
                Message m = new Message("/pw:" + password, true, username);
                SendNudes(m);
            }
        }
        public bool SendNudes(Message message)
        {
            if (!_socket.IsInit)
            {
                return false;
            }
            if (_socket.SendMessage(message))
                return true;
            return false;
        }

        public bool IsInit()
        {
            return _socket.IsInit;
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
                catch (ObjectDisposedException)
                {
                    Destroy();
                    break;
                }
                byte[] readBytes = new byte[_socket._socket.Available];
                int size = _socket._socket.Receive(readBytes);
                string received = Encoding.ASCII.GetString(readBytes);
                Console.WriteLine("Client - Received Raw: " + received);
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
