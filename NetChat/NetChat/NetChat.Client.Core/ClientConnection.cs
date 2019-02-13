using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetChat.Client.Core {
    public class ClientConnection
    {
        public string ConnectionUrl { get; }
        public int ConnectionPort { get; }
        public string Username { get; }
        public List<Message> RecievedMessages;
        private ClientSocket _netSocket;
        private Thread _updateMessages;
        public static bool ContinueWaiting = true;

        public ClientConnection(string connectionUrl, int connectionPort, string username, string password)
        {
            RecievedMessages = new List<Message>();
            ConnectionUrl = connectionUrl;
            ConnectionPort = connectionPort;
            Username = username;

            _netSocket = new ClientSocket(ConnectionUrl, connectionPort);
            if (!_netSocket.IsInit)
            {
                _netSocket = null;
            }
            else
            {
                _updateMessages = new Thread(ChatUpdater);
                _updateMessages.Start();
                var m = new Message("/pw:" + password, true, username);
                SendNudes(m);
            }
        }

        public bool SendNudes(Message message)
        {
            if (_netSocket == null)
                return false;
            if (!_netSocket.IsInit)
            {
                return false;
            }
            if (_netSocket.SendMessage(message))
                return true;
            return false;
        }

        public bool IsInit()
        {
            if (_netSocket == null)
                return false;
            return _netSocket.IsInit;
        }

        public void Destroy()
        {
            if (_netSocket != null)
            {
                _netSocket.DestroyConnection();
                while (_netSocket.IsInit) ;
                _netSocket = null;
            }
        }

        public void ChatUpdater()
        {
            if (!_netSocket.IsInit)
                return;
            while (ContinueWaiting)
            {
                try
                {
                    if (_netSocket.Socket.Available == 0)
                        continue;
                }
                catch (ObjectDisposedException)
                {
                    Destroy();
                    break;
                }
                catch(NullReferenceException)
                {
                    RecievedMessages.Add(new Message("Verbindung geschlossen", false, "INFO"));
                    break;
                }
                var readBytes = new byte[_netSocket.Socket.Available];
                var size = _netSocket.Socket.Receive(readBytes);
                var received = Encoding.UTF8.GetString(readBytes);
                Console.WriteLine("Client - Received Raw: " + received);
                var receivedMessage = new Message(received);
                if (receivedMessage.IsCommand)
                    HandleCommand(receivedMessage);
                else
                    RecievedMessages.Add(receivedMessage);
            }
        }

        private void HandleCommand(Message receivedMessage)
        {
            if(receivedMessage.Content == "/endConnection")
            {
                var endingMessage = new Message("Der Server wurde beendet und die Verbindung getrennt", true, "INFO");
                RecievedMessages.Add(endingMessage);
                Destroy();
            }
            if(receivedMessage.Content == "/pwKick")
            {
                var endingMessage = new Message("Der Server hat sie gekickt", true, "INFO");
                RecievedMessages.Add(endingMessage);
                Destroy();
            }
        }

        public bool SocketIsNull() {
            return _netSocket == null;
        }
    }
}
