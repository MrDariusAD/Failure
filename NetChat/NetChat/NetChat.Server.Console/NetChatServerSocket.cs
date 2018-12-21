using NetChat.Client;
using NetChat.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetChat.Server.Console
{
    public class NetChatServerSocket
    {

        public Socket Socket { get; set; }
        public IPEndPoint RemoteEp { get; set; }
        public IPAddress IpAddress { get; set; }
        public string RemoteIp { get; set; }
        public int ConnectedClients { get; set; }
        public Thread AccepterThread { get; set; }
        public List<Connection> Connections { get; set; }

        private string pw;

        public Thread NullClearerThread { get; set; }
        public bool ContinueAccepting = true;

        public void ClearNullThreads()
        {
            foreach (Connection nullConnection in Connections.Where(x => !x.Thread.IsAlive).ToList())
            {
                Connections.Remove(nullConnection);
            }
        }

        public void SendToOthers(Message toSend)
        {
            List<Connection> ToBeRemoved = new List<Connection>();
            foreach (Connection others in Connections)
            {
                try
                {
                    others.SendMessage(toSend);
                }
                catch (Exception)
                {
                    ToBeRemoved.Add(others);
                }
            }
            foreach (Connection DeadConnection in ToBeRemoved)
            {
                DeadConnection.Close();
                Connections.Remove(DeadConnection);
            }
        }

        internal void HandleCommand(Message receivedMessage)
        {
            if (receivedMessage.Content.ToLower().Contains("/kill"))
                DestroyServer();
            if (receivedMessage.Content.ToLower().Contains("/msg"))
            {
                String msg = receivedMessage.Content.Substring(5);
                if (String.IsNullOrEmpty(msg))
                    return;
                Message m = new Message(msg, false, "Server");
                SendToOthers(m);
            }
        }

        public void StartNullClearerThread()
        {
            NullClearerThread.Start();
        }
        public void StopNullClearerThread()
        {
            NullClearerThread.Abort();
        }

        public void EndConnection(Connection deadConnection)
        {
            Connections.Remove(deadConnection);

        }

        public NetChatServerSocket(string ip, int port, String pw)
        {
            this.pw = pw;
            NullClearerThread = new Thread(ClearNullThreads);
            Connections = new List<Connection>();
            ConnectedClients = 0;
            InitSocket(GetLocalIPAddress(), port);
            RemoteIp = RemoteEp.Address.ToString();
        }

        public static string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void DestroyServer()
        {
            Message endingMessage = new Message("/endConnection", true, "Server");
            SendToOthers(endingMessage);
            ContinueAccepting = false;
            Socket.Close();
            foreach (Connection c in Connections)
            {
                c.Close();
                c.StopThread();
            }
        }

        private void InitSocket(string ipAdressString, int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Logger.Debug("Init Server on " + ipAdressString + ":" + port);
            IpAddress = IPAddress.Parse(ipAdressString);
            RemoteEp = new IPEndPoint(IpAddress, port);
            Socket.Bind(RemoteEp);
        }

        public void StartListening()
        {
            Socket.Listen(25);
            AccepterThread = new Thread(ConnectionAccepter);
            AccepterThread.Start();
        }

        public void StopListening()
        {
            AccepterThread.Abort();
        }

        private void ConnectionAccepter()
        {
            while (ContinueAccepting)
            {
                try
                {
                    Socket worksocket = Socket.Accept();
                    Connection connection = new Connection(worksocket, this, pw);
                    System.Console.WriteLine($"Neue Connection: {RemoteIp}");
                    Connections.Add(connection);
                    ConnectedClients++;
                }
                catch (SocketException)
                {
                    ContinueAccepting = false;
                }
            }
        }
    }
}