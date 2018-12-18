using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NetChat.Client.Core {
    class NetChatSocket
    {
        public Socket _socket;
        private IPAddress _ipAdress;
        private IPEndPoint _remoteEp;
        public bool IsInit { get; private set; }

        public NetChatSocket(string ipAdressString, int port)
        {
            try
            {
                InitSocket(ipAdressString, port);
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Verbindung ist fehlgeschlagen", "Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IsInit = true;
        }

        public void InitSocket(string ipAdressString, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _ipAdress = IPAddress.Parse(ipAdressString);
            _remoteEp = new IPEndPoint(_ipAdress, port);
            _socket.Connect(_remoteEp);
        }

        public void SendMessage(Message message)
        {
            byte[] messageAsBytes = Encoding.ASCII.GetBytes(message.ToString());
            try
            {
                _socket.Send(messageAsBytes);
            }catch(SocketException se)
            {
                DestroyConnection();
                IsInit = false;
            }
        }

        public void DestroyConnection()
        {
            _socket.Close();
        }
    }
}
