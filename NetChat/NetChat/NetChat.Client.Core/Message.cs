using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core {
    public class Message {
        public Message(string content, bool isCommand, User fromUser, Friend targetFriend) {
            Content = content ?? "";
            IsCommand = isCommand;
            FromUser = fromUser;
            TargetFriend = targetFriend;
        }
        public User FromUser { get; }
        public Friend TargetFriend { get; }

        public string Content { get; set; }
        public bool IsCommand { get; }
        public override string ToString() {
            return $"--{IsCommand}//<{FromUser.Username}>\\\\<{TargetFriend.Username}>{{{Content}}}//<EOF>";
        }
    }
}
