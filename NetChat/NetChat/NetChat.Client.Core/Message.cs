using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core {
    public class Message {
        public Message(string content, bool isCommand, string username) {
            Content = content ?? "";
            IsCommand = isCommand;
            Username = username;
        }
        public string Username { get; }
        public string Content { get; set; }
        public bool IsCommand { get; }
        public override string ToString() {
            return $"--{IsCommand}//<{Username}>\\\\{{{Content}}}//<EOF>";
        }
    }
}
