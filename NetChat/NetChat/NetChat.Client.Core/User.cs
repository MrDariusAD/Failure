using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Client.Core {
    public class User {
        public User(string username, string passwordHash) {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Friends = new List<Friend>();
        }

        public string Username { get; }
        private string PasswordHash { get; }
        public List<Friend> Friends { get; set; }

        public bool CheckPassword(string passwordInputHash) {
            return passwordInputHash.Equals(PasswordHash);
        }
    }
}
