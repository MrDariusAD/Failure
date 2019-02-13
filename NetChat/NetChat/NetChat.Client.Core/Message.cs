using System;

namespace NetChat.Client.Core {
    public class Message {

        public string Username { get; }
        public string Content { get; set; }
        public bool IsCommand { get; }

        public Message(string content, bool isCommand, string username) {
            Content = content ?? "";
            IsCommand = isCommand || content != null && content[0] == '/';
            Username = username;
            if(content != null && content.Contains("\n"))
            {
                Content = content.Replace("\n", "");
            }
            if (username != null && username.Contains("\n"))
            {
                Username = username.Replace("\n", "");
            }
        }

        public Message(string receivedMessage) {
            string[] seperator = {"-/-"};
            var proerties = receivedMessage.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            IsCommand = proerties[0].ToUpper().Contains("TRUE");
            Username = proerties[1];
            Content = proerties[2].Replace("#\\#", "");

        }
        public override string ToString() {
            return $"{IsCommand}-/-{Username}-/-{Content}#\\#";
        }
    }
}
