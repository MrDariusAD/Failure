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
    public class ServerSocket
    {

        public Socket Socket { get; set; }
        public IPEndPoint RemoteEp { get; set; }
        public IPAddress IpAddress { get; set; }
        public string RemoteIp { get; set; }
        public int ConnectedClients { get; set; }
        public Thread AccepterThread { get; set; }
        public List<ServerConnection> Connections { get; set; }
        public bool isRunning = true;

        private readonly string pw;

        public Thread NullClearerThread { get; set; }
        public bool ContinueAccepting = true;

        public void ClearNullThreads()
        {
            foreach (ServerConnection nullConnection in Connections.Where(x => !x.Thread.IsAlive).ToList())
            {
                Connections.Remove(nullConnection);
            }
        }

        public void SendToOthers(Message toSend)
        {
            List<ServerConnection> ToBeRemoved = new List<ServerConnection>();
            foreach (ServerConnection others in Connections)
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
            foreach (ServerConnection DeadConnection in ToBeRemoved)
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

        public void EndConnection(ServerConnection deadConnection)
        {
            Connections.Remove(deadConnection);

        }

        public ServerSocket(int port, String pw)
        {
            this.pw = pw;
            NullClearerThread = new Thread(ClearNullThreads);
            Connections = new List<ServerConnection>();
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

        /// <summary>
        /// Beendet den Server
        /// </summary>
        public void DestroyServer()
        {
            Message endingMessage = new Message("/endConnection", true, "Server");
            SendToOthers(endingMessage);
            ContinueAccepting = false;
            Socket.Close();
            foreach (ServerConnection c in Connections)
            {
                c.Close();
                c.StopThread();
            }
            isRunning = false;
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
            throw new NotImplementedException("Die 'Thread.Abort()'-Methode hängt sich immer auf - Nicht benutzen ^w^ ▄︻̷┻̿═━一😉 ☠ ☭");
            AccepterThread.Abort();
        }

        private void ConnectionAccepter()
        {
            while (ContinueAccepting)
            {
                try
                {
                    Socket worksocket = Socket.Accept();
                    ServerConnection connection = new ServerConnection(worksocket, this, pw);
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