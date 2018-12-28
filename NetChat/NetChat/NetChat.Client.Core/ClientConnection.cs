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
        private ClientSocket NetSocket;
        private Thread updateMessages;
        public static bool ContinueWaiting = true;

        public ClientConnection(string connectionUrl, int connectionPort, string username, string password)
        {
            RecievedMessages = new List<Message>();
            ConnectionUrl = connectionUrl;
            ConnectionPort = connectionPort;
            Username = username;

            NetSocket = new ClientSocket(ConnectionUrl, connectionPort);
            if (!NetSocket.IsInit)
            {
                NetSocket = null;
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
            if (NetSocket == null)
                return false;
            if (!NetSocket.IsInit)
            {
                return false;
            }
            if (NetSocket.SendMessage(message))
                return true;
            return false;
        }

        public bool IsInit()
        {
            if (NetSocket == null)
                return false;
            return NetSocket.IsInit;
        }

        public void Destroy()
        {
            if (NetSocket != null)
            {
                NetSocket.DestroyConnection();
                while (NetSocket.IsInit) ;
                NetSocket = null;
            }
        }

        public void ChatUpdater()
        {
            if (!NetSocket.IsInit)
                return;
            while (ContinueWaiting)
            {
                try
                {
                    if (NetSocket._socket.Available == 0)
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
                byte[] readBytes = new byte[NetSocket._socket.Available];
                int size = NetSocket._socket.Receive(readBytes);
                string received = Encoding.UTF8.GetString(readBytes);
                Console.WriteLine("Client - Received Raw: " + received);
                Message receivedMessage = new Message(received);
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
                Message endingMessage = new Message("Der Server wurde beendet und die Verbindung getrennt", true, "INFO");
                RecievedMessages.Add(endingMessage);
                Destroy();
            }
            if(receivedMessage.Content == "/pwKick")
            {
                Message endingMessage = new Message("Der Server hat sie gekickt", true, "INFO");
                RecievedMessages.Add(endingMessage);
                Destroy();
            }
        }

        public bool SocketIsNull()
        {
            if (NetSocket == null)
                return true;
            return false;
        }
    }
}
