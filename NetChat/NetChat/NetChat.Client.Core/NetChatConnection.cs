using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core
{
    public class Connection
    {
        public Connection(string connectionUrl, int connectionPort, string username) {
            ConnectionUrl = connectionUrl;
            ConnectionPort = connectionPort;
            Username = username;
            _socket = new NetChatSocket(ConnectionUrl, connectionPort);
        }
        public string ConnectionUrl { get; }
        public int ConnectionPort { get; }
        public string Username { get; }
        private NetChatSocket _socket;
        public void SendNudes(string messageString) {
            if (!_socket.IsInit) throw new Exception("Uninitialized Socket");
            var message = new Message(messageString, false, Username);
            _socket.SendMessage(message);

        }

    }
}
