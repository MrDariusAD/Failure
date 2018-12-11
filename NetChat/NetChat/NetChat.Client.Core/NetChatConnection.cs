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
        public Connection(string connectionUrl, int connectionPort, User connectedUser, string passwordInput) {
            ConnectionUrl = connectionUrl;
            ConnectionPort = connectionPort;
            ConnectedUser = connectedUser;
            _socket = new NetChatSocket(ConnectionUrl, connectionPort);
            IsAuthenticated = Authenticate(passwordInput);
        }
        public string ConnectionUrl { get; }
        public int ConnectionPort { get; }
        public User ConnectedUser { get; }
        private NetChatSocket _socket;
        public bool IsAuthenticated { get; }

        //TODO: passwordInput muss in Authenticate in einen Hash umgewandelt werden (MD5)
        public bool Authenticate(string passwordInput) {
            return ConnectedUser.CheckPassword(passwordInput);
        }

        public void SendNudes(string messageString, Friend targetFriend) {
            if (!_socket.IsInit) throw new Exception("Uninitialized Socket");
            var message = new Message(messageString, false, ConnectedUser, targetFriend);
            _socket.SendMessage(message);
            ConnectedUser.Friends.Where(x => x.Username == targetFriend.Username).ToList()[0]?.Messages.Add(message);

        }

    }
}
