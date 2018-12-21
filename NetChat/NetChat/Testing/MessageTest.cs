using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetChat.Client.Core;
using Xunit;

namespace NetChat.Client.Core {

    public class MessageTest {

        [Fact]
        public void Message_ctor_String_wird_uebergeben_Korrektes_Message_Objekt_ist_das_Ergebnis() {
            //Arrange
            string receivedMessage = "False-/-I3lackRacer-/-test";
            Message message;

            //Act
            message = new Message(receivedMessage);

            //Assert
            Assert.Contains("I3lackRacer", message.Username);
            Assert.Contains("test", message.Content);
            Assert.False(message.IsCommand);
        }
    }
}
