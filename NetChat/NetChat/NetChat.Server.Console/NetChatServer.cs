using System;

namespace NetChat.Server.Console {

    public class NetChatServer {

        public  ServerSocket Socket { get; }

        public NetChatServer(int port, string pw) {
            Socket = new ServerSocket(port, pw);
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

        public bool isRunning()
        {
            if (Socket == null)
                return false;
            if (!Socket.isRunning)
                return false;
            return true;
        }
    }
}