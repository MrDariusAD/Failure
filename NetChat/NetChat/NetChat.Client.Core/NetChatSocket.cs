using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NetChat.Client.Core {
    public class NetChatSocket
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
            catch (Exception)
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

        public bool SendMessage(Message message)
        {
            byte[] messageAsBytes = Encoding.ASCII.GetBytes(message.ToString());
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

        private bool SocketConnected()
        {
            bool part1 = _socket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (_socket.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        public void DestroyConnection()
        {
            _socket.Close();
        }
    }
}