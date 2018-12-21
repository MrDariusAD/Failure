namespace NetChat.Server.Console {
    class Program {
        static void Main(string[] args) {
            NetChatServer server = new NetChatServer("127.0.0.1", 4308, "Passwort");
            System.Console.WriteLine("Server wurde erstellt");
            System.Console.WriteLine("Starte Server");
            server.StartServer();
            server.Socket.AccepterThread.Join();
        }
    }
}
