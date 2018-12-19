﻿using System;
using System.Security.AccessControl;
using Xunit.Sdk;

namespace NetChat.Client.Core {
    public class Message {

        public string Username { get; }
        public string Content { get; set; }
        public bool IsCommand { get; }

        public Message(string content, bool isCommand, string username) {
            Content = content ?? "";
            IsCommand = isCommand;
            if (content[0] == '/')
                this.IsCommand = true;
            Username = username;
        }

        public Message(string receivedMessage) {
            string[] seperator = {"-/-"};
            var proerties = receivedMessage.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            IsCommand = proerties[0].ToUpper().Contains("TRUE");
            Username = proerties[1];
            Content = proerties[2];

        }
        public override string ToString() {
            return $"{IsCommand}-/-{Username}-/-{Content}";
        }
    }
}
