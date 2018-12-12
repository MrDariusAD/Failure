using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NetChat.Server.Console {

    public class NetChatServer {

        public  NetChatServerSocket Socket { get; }

        public NetChatServer(int port) {
            Socket = new NetChatServerSocket(port);
        }

        public void StartServer() {
            Socket.StartListening();
            Socket.StartNullClearerThread();
        }

        public void Destroy()
        {
            Socket.StopNullClearerThread();
            Socket.DestroyServer();
        }
    }
}
