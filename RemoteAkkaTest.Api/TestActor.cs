using Akka.Actor;
using RemoteAkkaTest.Server;

namespace RemoteAkkaTest.Api
{
    public class AskForTest
    {
        public string Message { get; set; }
    }


    public class TestActor : ReceiveActor
    {
        private readonly ActorSelection _server = Context.ActorSelection(
        //"akka.tcp://MyServer@localhost:8081/user/ServerTestActor"
        "akka.tcp://MyServer@server:8081/user/ServerTestActor"
        );

        public TestActor()
        {
            Receive<AskForTest>(message => HandleAskForTest(message));
        }

        private void HandleAskForTest(AskForTest message)
        {
            Console.WriteLine("Received message");
            //var result = _server.Ask("Testing a message...", TimeSpan.FromSeconds(5)).Result;
            //var result = _server.Ask(new { Test = true, Message = "This is a test!" }, TimeSpan.FromSeconds(5)).Result;
            var result = _server.Ask(new TestMessage { Message = message.Message });
            Sender.Tell(result);
        }
    }
}
