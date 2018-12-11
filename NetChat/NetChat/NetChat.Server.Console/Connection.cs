using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetChat.Client.Core;

namespace NetChat.Server.Console {
    class Connection
    {
        public Thread Thread { get; set; }
        public string Username { get; }
        public List<Message> recievedMessages { get; set; }

        public void ProcessMessages() {

        }
    }
}
