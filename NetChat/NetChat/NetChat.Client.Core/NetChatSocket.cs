using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core {
    class NetChatSocket {
        private Socket _socket;
        private IPAddress _ipAdress;
        private IPEndPoint _remoteEp;
        public bool IsInit { get; }
        public NetChatSocket(string ipAdressString, int port) {
            InitSocket(ipAdressString, port);
            IsInit = true;
        }

        public void InitSocket(string ipAdressString, int port) {
            _socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _ipAdress = IPAddress.Parse(ipAdressString);
            _remoteEp = new IPEndPoint(_ipAdress, port);
            _socket.Connect(_remoteEp);
        }

        public void SendMessage(Message message) {
            var messageAsBytes = Encoding.ASCII.GetBytes(message.ToString());
            _socket.Send(messageAsBytes);
        }

        public void DestroyConnection() {
            _socket.Close();
        }
    }
}
