using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core {
    public class Friend {
        public string Username { get; set; }
        public List<Message> Messages { get; set; }
    }
}
