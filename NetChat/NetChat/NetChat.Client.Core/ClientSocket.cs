using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NetChat.Client.Core {
    public class ClientSocket
    {
        public Socket _socket;
        private IPAddress _ipAdress;
        private IPEndPoint _remoteEp;
        public bool IsInit { get; private set; }

        public ClientSocket(string ipAdressString, int port)
        {
            try
            {
                InitSocket(ipAdressString, port);
            }
            catch (Exception e)
            {
                Logger.Error(e);
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

        public bool SendMessage(Message message)
        {
            byte[] messageAsBytes = Encoding.UTF8.GetBytes(message.ToString());
            try
            {
                _socket.Send(messageAsBytes);
                return true;
            }
            catch (SocketException)
            {
                DestroyConnection();
                IsInit = false;
                return false;
            }
            catch (ObjectDisposedException e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public void DestroyConnection()
        {
            _socket.Close();
            IsInit = false;
        }
    }
}