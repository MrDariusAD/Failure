using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace NetChat.Client.Core {
    public class ClientSocket
    {
        public Socket Socket;
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
                MessageBox.Show($"Die Verbindung ist fehlgeschlagen\n\nERROR: '{e.Message}'", @"Verbindung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IsInit = true;
        }

        public void InitSocket(string ipAdressString, int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _ipAdress = IPAddress.Parse(ipAdressString);
            _remoteEp = new IPEndPoint(_ipAdress, port);
            Socket.Connect(_remoteEp);
        }

        public bool SendMessage(Message message)
        {
            var messageAsBytes = Encoding.UTF8.GetBytes(message.ToString());
            try
            {
                Socket.Send(messageAsBytes);
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
            Console.WriteLine(1);
            Socket.Close();
            Console.WriteLine(2);
            IsInit = false;
        }
    }
}