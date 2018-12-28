using System;
using System.Collections.Generic;
using System.Linq;
using NetChat.Client.Core;
using NetChat.Server.Console;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace Testing
{
    public class TestFullConnection
    {

        /// <summary>
        /// Erstellt einen Server.
        /// Verbindet sich zum Server.
        /// Sendet 'Test'
        /// Empfängt 'Test'
        /// </summary>
        [Fact]
        public void TestConnection()
        {
            ServerSocket netChatServerSocket = new ServerSocket(4308, "passwort");
            ClientConnection netChatConnection;
            netChatServerSocket.StartListening();
            netChatServerSocket.StartNullClearerThread();
            netChatConnection = new ClientConnection(ServerSocket.GetLocalIPAddress(), 4308, "I3lackRacer", "passwort");

            Message message = new Message("Test", false, "I3lackRacer");
            netChatConnection.SendNudes(message);
            while (netChatConnection.RecievedMessages.Count == 0);
            Console.WriteLine(netChatConnection.RecievedMessages.ToString());
            Console.WriteLine(netChatConnection.RecievedMessages[0]);

            Assert.Contains("I3lackRacer ist den Chatroom beigetreten", netChatConnection.RecievedMessages[0].Content);
            Assert.Contains("Test", netChatConnection.RecievedMessages[1].Content);
            
            netChatServerSocket.DestroyServer();
            netChatConnection.Destroy();

            Assert.True(netChatConnection.SocketIsNull());
        }
    }
}
