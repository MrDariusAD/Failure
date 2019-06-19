
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

        public bool IsRunning() {
            return Socket != null && Socket.IsRunning;
        }
    }
}