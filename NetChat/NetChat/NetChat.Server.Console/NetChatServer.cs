using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetChat.Client.Core;

namespace NetChat.Server.Console {
    class NetChatServer {
        public NetChatServer(NetChatServerSocket socket, int port) {
            Socket = socket;
            Port = port;
        }
        private int Port { get; }
        public  NetChatServerSocket Socket { get; }
        public List<Thread> Threads { get; set; }

        public void ClearNullThreads() {
            foreach (var nullThread in Threads.Where(x => !x.IsAlive).ToList()) {
                Threads.Remove(nullThread);
            }
        }

    }
}
