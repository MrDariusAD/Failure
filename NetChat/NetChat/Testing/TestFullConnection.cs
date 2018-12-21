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
        [Fact]
        public void TestConnection()
        {
            NetChatServerSocket netChatServerSocket = new NetChatServerSocket("127.0.0.1", 4308, "passwort");
            NetChatConnection netChatConnection;
            netChatServerSocket.StartListening();
            netChatServerSocket.StartNullClearerThread();
            netChatConnection = new NetChatConnection(NetChatServerSocket.GetLocalIPAddress(), 4308, "I3lackRacer", "passwort");

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
