using Akka.Actor;

namespace RemoteAkkaTest.Server
{
    public class TestMessage
    {
        public string Message { get; set; }
    }

    public class TestActor : ReceiveActor
    {
        public TestActor()
        {
            Receive<TestMessage>(message => HandleMessage(message));
            //Receive<object>(message => HandleMessage(message));
            //Receive<string>(message => HandleMessage(message));
        }

        private void HandleMessage(TestMessage message)
        {
            Console.WriteLine("Received a message");
            Sender.Tell(new TestMessage
            {
                Message = $"I handled the message {message}"
            });
        }
    }
}
