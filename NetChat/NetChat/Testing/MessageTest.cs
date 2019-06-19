using Xunit;

namespace NetChat.Client.Core {

    public class MessageTest {

        [Fact]
        public void Message_ctor_String_wird_uebergeben_Korrektes_Message_Objekt_ist_das_Ergebnis() {
            //Arrange
            const string receivedMessage = "False-/-I3lackRacer-/-test";

            //Act
            var message = new Message(receivedMessage);

            //Assert
            Assert.Contains("I3lackRacer", message.Username);
            Assert.Contains("test", message.Content);
            Assert.False(message.IsCommand);
        }
    }
}
