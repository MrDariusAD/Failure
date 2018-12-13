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
            var receivedMessage = "--{false}//<MrDariusAD>\\\\{{Hello my baby}}//<EOF>";
            Message message;

            //Act
            message = new Message(receivedMessage);

            //Assert
            message.Username.Should().Be("MrDariusAD");
            message.Content.Should().Be("Hello my baby");
            message.IsCommand.Should().Be(false);

        }
    }
}
