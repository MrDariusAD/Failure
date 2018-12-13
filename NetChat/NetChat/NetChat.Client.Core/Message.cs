namespace NetChat.Client.Core {
    public class Message {

        public string Username { get; }
        public string Content { get; set; }
        public bool IsCommand { get; }

        public Message(string content, bool isCommand, string username) {
            Content = content ?? "";
            IsCommand = isCommand;
            Username = username;
        }

        public Message(string receivedMessage) {
            IsCommand = receivedMessage.Substring(3, receivedMessage.IndexOf('/', 3)) == "True";
            Username = receivedMessage.Substring(receivedMessage.IndexOf('<'),
                (receivedMessage.IndexOf('>') - receivedMessage.IndexOf('<')));
            Content = receivedMessage.Substring(receivedMessage.IndexOf('{'),
                (receivedMessage.IndexOf('}') - receivedMessage.IndexOf('{')));
        }
        public override string ToString() {
            return $"--{IsCommand}//<{Username}>\\\\{{{Content}}}//<EOF>";
        }
    }
}
