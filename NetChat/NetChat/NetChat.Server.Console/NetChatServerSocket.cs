using System.Net;
using System.Net.Sockets;
using System.Text;
using NetChat.Client.Core;

namespace NetChat.Server.Console {
    public class NetChatServerSocket {

        public Socket _socket;
        public IPEndPoint _remoteEp;
        public IPAddress _ipAddress;
        public NetChatServerSocket (int port) {
            InitSocket("127.0.0.1", port);
        }

        public void InitSocket(string ipAdressString, int port) {
            _socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _ipAddress = IPAddress.Parse(ipAdressString);
            _remoteEp = new IPEndPoint(_ipAddress, port);
            _socket.Bind(_remoteEp);
        }

    }
}