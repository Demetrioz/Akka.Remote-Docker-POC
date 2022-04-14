using Akka.Actor;
using Akka.Configuration;

namespace RemoteAkkaTest.Server
{
    // https://github.com/akkadotnet/akka.net/blob/dev/src/examples/Chat/ChatServer/Program.cs

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ActorSystem _actorSystem;
        private IActorRef _actorRef;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = ConfigurationFactory.ParseString(@"
                akka {  
                    actor {
                        provider = remote
                    }
                    remote {
                        dot-netty.tcp {
                            port = 8081
                            hostname = 0.0.0.0
                            public-hostname = server
                        }
                    }
                }
                ");

            _actorSystem = ActorSystem.Create("MyServer", config);
            _actorRef = _actorSystem.ActorOf(Props.Create(() => new TestActor()), "ServerTestActor");
            Console.WriteLine("Actor system created...");
            Console.ReadLine();
        }
    }
}