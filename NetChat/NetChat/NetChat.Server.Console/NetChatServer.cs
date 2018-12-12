using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace NetChat.Server.Console {
    public class NetChatServer {
        public NetChatServer(int port) {
            Socket = new NetChatServerSocket(port);
        }
        public  NetChatServerSocket Socket { get; }

        public void StartServer() {
            Socket.StartListening();
            Socket.StartNullClearerThread();
        }

    }
}
