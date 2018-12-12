using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Server.Console {
    class Program {
        static void Main(string[] args) {
            var server = new NetChatServer(4308);
            System.Console.WriteLine("Server wurde erstellt");
            System.Console.WriteLine("Starte Server");
            server.StartServer();
            server.Socket.AccepterThread.Join();
        }
    }
}
