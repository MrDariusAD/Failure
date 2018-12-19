using System;

namespace NetChat.Server.Console {

    public class NetChatServer {

        public  NetChatServerSocket Socket { get; }

        public NetChatServer(String ip, int port, string pw) {
            Socket = new NetChatServerSocket(ip, port, pw);
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