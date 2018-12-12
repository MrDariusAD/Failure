using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NetChat.Client.Core;

namespace NetChat.Server.Console {
    public class NetChatServerSocket {

        public Socket Socket { get; set; }
        public IPEndPoint RemoteEp { get; set; }
        public IPAddress IpAddress { get; set; }
        public string RemoteIp { get; set; }
        public int ConnectedClients { get; set; }
        public Thread AccepterThread { get; set; }
        public List<Connection> Connections { get; set; }
        public Thread NullClearerThread { get; set; }
        public void ClearNullThreads() {
            foreach (var nullConnection in Connections.Where(x => !x.Thread.IsAlive).ToList()) {
                Connections.Remove(nullConnection);
            }
        }

        public void StartNullClearerThread() {
            NullClearerThread.Start();
        }
        public void StopNullClearerThread() {
            NullClearerThread.Abort();
        }

        public NetChatServerSocket (int port) {
            NullClearerThread = new Thread(ClearNullThreads);
            Connections = new List<Connection>();
            ConnectedClients = 0;
            InitSocket("127.0.0.1", port);
            RemoteIp = RemoteEp.Address.ToString();
        }

        public void InitSocket(string ipAdressString, int port) {
            Socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IpAddress = IPAddress.Parse(ipAdressString);
            RemoteEp = new IPEndPoint(IpAddress, port);
            Socket.Bind(RemoteEp);
        }

        public void StartListening() {
            Socket.Listen(25);
            AccepterThread = new Thread(ConnectionAccepter);
            AccepterThread.Start();
        }

        public void StopListening() {
            AccepterThread.Abort();
        }

        public void ConnectionAccepter() {
            while (true) {
                var worksocket = Socket.Accept();
                var connection = new Connection(worksocket);
                System.Console.WriteLine($"Neue Connection: {RemoteIp}");
                Connections.Add(connection);
                ConnectedClients++;
            }
        }
    }
}