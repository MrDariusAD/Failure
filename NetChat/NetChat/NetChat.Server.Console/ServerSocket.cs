using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetChat.Client.Core;

namespace NetChat.Server.Console
{
    public class ServerSocket
    {
        private readonly string _pw;
        public bool ContinueAccepting = true;
        public bool IsRunning = true;
        public Socket Socket { get; set; }
        public IPEndPoint RemoteEp { get; set; }
        public IPAddress IpAddress { get; set; }
        public string RemoteIp { get; set; }
        public int ConnectedClients { get; set; }
        public Thread AccepterThread { get; set; }
        public List<ServerConnection> Connections { get; set; }

        public ServerSocket(int port, string pw) {
            _pw = pw;
            NullClearerThread = new Thread(ClearNullThreads);
            Connections = new List<ServerConnection>();
            ConnectedClients = 0;
            InitSocket(GetLocalIpAddress(), port);
            RemoteIp = RemoteEp.Address.ToString();
        }

        public ServerConnection GetServerConnectionByName(string username)
        {
            foreach(ServerConnection connection in Connections)
            {
                if (connection.Username == username)
                    return connection;
            }
            return null;
        }

        public Thread NullClearerThread { get; set; }

        public void ClearNullThreads() {
            foreach (var nullConnection in Connections.Where(x => !x.Thread.IsAlive).ToList())
                Connections.Remove(nullConnection);
        }

        public void SendToOthers(Message toSend) {
            var toBeRemoved = new List<ServerConnection>();
            foreach (var others in Connections)
                try {
                    others.SendMessage(toSend);
                }
                catch (Exception) {
                    toBeRemoved.Add(others);
                }

            foreach (var deadConnection in toBeRemoved) {
                deadConnection.Close();
                Connections.Remove(deadConnection);
            }
        }

        internal void HandleCommand(Message receivedMessage) {
            if (receivedMessage.Content.ToLower().Contains("/kill"))
                DestroyServer();
            if (receivedMessage.Content.ToLower().Contains("/list"))
            {
                string userlist = "Users: Server ";
                foreach(ServerConnection connection in Connections)
                {
                    userlist += connection.Username + " ";
                }
                Message message = new Message(userlist, false, "Server");
                ServerConnection toBeSendConnection = GetServerConnectionByName(receivedMessage.Username);
                toBeSendConnection?.SendMessage(message);
            } 
            if (receivedMessage.Content.ToLower().Contains("/msg")) {
                var msg = receivedMessage.Content.Substring(5);
                if (string.IsNullOrEmpty(msg))
                    return;
                var m = new Message(msg, false, "Server");
                SendToOthers(m);
            }
        }

        public void StartNullClearerThread() {
            NullClearerThread.Start();
        }

        public void StopNullClearerThread() {
            NullClearerThread.Abort();
        }

        public static string GetLocalIpAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        ///     Beendet den Server
        /// </summary>
        public void DestroyServer() {
            var endingMessage = new Message("/endConnection", true, "Server");
            SendToOthers(endingMessage);
            ContinueAccepting = false;
            Socket.Close();
            foreach (var c in Connections) {
                c.Close();
                c.StopThread();
            }

            IsRunning = false;
        }

        private void InitSocket(string ipAdressString, int port) {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Logger.Debug("Init Server on " + ipAdressString + ":" + port);
            IpAddress = IPAddress.Parse(ipAdressString);
            RemoteEp = new IPEndPoint(IpAddress, port);
            Socket.Bind(RemoteEp);
        }

        public void StartListening() {
            Socket.Listen(25);
            AccepterThread = new Thread(ConnectionAccepter);
            AccepterThread.Start();
        }

        private void ConnectionAccepter() {
            while (ContinueAccepting)
                try {
                    var worksocket = Socket.Accept();
                    var connection = new ServerConnection(worksocket, this, _pw);
                    System.Console.WriteLine($"Neue Connection: {RemoteIp}");
                    Connections.Add(connection);
                    ConnectedClients++;
                }
                catch (SocketException) {
                    ContinueAccepting = false;
                }
        }
    }
}