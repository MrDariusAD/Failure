using System;

namespace NetChat.Server.Console {

    public class NetChatServer {

        public  NetChatServerSocket Socket { get; }

        public NetChatServer(String ip, int port) {
            Socket = new NetChatServerSocket(ip, port);
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