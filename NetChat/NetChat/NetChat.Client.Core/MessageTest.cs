using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NetChat.Client.Core;
using Xunit;

namespace NetChat.Client.Core {

    public class MessageTest {

        [Fact]
        public void Message_ctor_String_wird_uebergeben_Korrektes_Message_Objekt_ist_das_Ergebnis() {
            //Arrange
            var receivedMessage = "False-/-I3lackRacer-/-test";
            Message message;

            //Act
            message = new Message(receivedMessage);

            //Assert
            message.Username.Should().Be("I3lackRacer");
            message.Content.Should().Be("test");
            message.IsCommand.Should().Be(false);

        }
    }
}
