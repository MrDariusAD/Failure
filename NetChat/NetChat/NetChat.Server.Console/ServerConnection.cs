using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NetChat.Client.Core;

namespace NetChat.Server.Console
{
    public class ServerConnection
    {
        private readonly string _pw;
        private bool _continueReceiving = true;
        private bool _validatedViaPassword;

        public ServerConnection(Socket socket, ServerSocket serverSocket, string pw) {
            _pw = pw;
            ServerSocket = serverSocket;
            Socket = socket ?? throw new ArgumentNullException(nameof(socket));
            Thread = new Thread(ProcessMessages);
            RecievedMessages = new List<Message>();
            StartThread();
        }

        private ServerSocket ServerSocket { get; }
        public Socket Socket { get; set; }
        public Thread Thread { get; set; }
        private string Username { get; set; }
        private List<Message> RecievedMessages { get; }

        public void ProcessMessages() {
            while (_continueReceiving) {
                Thread.CurrentThread.Join(20);
                try {
                    if (Socket.Available == 0)
                        continue;
                }
                catch (ObjectDisposedException) {
                    Close();
                    break;
                }
                try
                {
                    var readBytes = new byte[Socket.Available];
                    Socket.Receive(readBytes);
                    var received = Encoding.UTF8.GetString(readBytes);
                    System.Console.WriteLine($"Server - Neue Nachricht erhalten: {received}");
                    Logger.Debug($"Server - Neue Nachricht erhalten: {received}");
                    // Für den Fall das während der Verarbeitungszeit 2 Nachrichten reingekommen sind
                    var proerties = received.Split("#\\#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (var m in proerties)
                        // Es wird gebrüft ob der string was enhällt weil das letzte Feld immer leer sein wird
                        if (m.Length > 1)
                            SingleMessage(m);
                }catch(Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        public void SendMessage(string message, bool command = false) {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var m = new Message(message, command, "Server");
            SendMessage(m);
        }

        public void SendMessage(Message message) {
            var kickMsg = Encoding.UTF8.GetBytes(message.ToString());
            Socket.Send(kickMsg);
        }

        private void SingleMessage(string received) {
            var receivedMessage = new Message(received);
            CheckNameChange(receivedMessage);
            if (receivedMessage.IsCommand) {
                if (receivedMessage.Content.StartsWith("/pw:")) {
                    if (!IsPasswortValide(receivedMessage.Content)) {
                        SendMessage("Das Passwort ist falsch");
                        Close();
                    }
                    else {
                        _validatedViaPassword = true;
                        var m = new Message(receivedMessage.Username + " ist den Chatroom beigetreten", false,
                            "Server");
                        ServerSocket.SendToOthers(m);
                    }
                }
                else {
                    ServerSocket.HandleCommand(receivedMessage);
                }
            }
            else {
                if (!_validatedViaPassword) {
                    SendMessage("/pwKick", true);
                    Thread.Join(20);
                    Close();
                }

                ServerSocket.SendToOthers(receivedMessage);
                RecievedMessages.Add(receivedMessage);
            }
        }

        private void CheckNameChange(Message receivedMessage) {
            if (Username == null) {
                Username = receivedMessage.Username;
            }
            else {
                if (Username != receivedMessage.Username) {
                    var nameChangeMessage =
                        new Message(Username + " hat sich zu " + receivedMessage.Username + " umbenannt", false,
                            "Server");
                    ServerSocket.SendToOthers(nameChangeMessage);
                    Username = receivedMessage.Username;
                }
            }
        }

        private bool IsPasswortValide(string pw) {
            pw = pw.Substring(4);
            System.Console.WriteLine("PW: " + pw);
            if (_pw == pw)
                return true;
            return false;
        }

        public void StartThread() {
            Thread.Start();
        }

        public void StopThread() {
            _continueReceiving = false;
        }

        internal void Close() {
            Socket.Close();
        }
    }
}