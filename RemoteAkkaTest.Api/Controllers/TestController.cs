using Akka.Actor;
using Microsoft.AspNetCore.Mvc;

namespace RemoteAkkaTest.Api.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController : ControllerBase
    {
        private readonly IActorRef RootActor;

        public TestController(IActorRef rootActor)
        {
            RootActor = rootActor;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var result = await RootActor.Ask(
                new AskForTest { Message = "This is from TestController" },
                TimeSpan.FromSeconds(5)
            );

            return Ok(result);
        }
    }
}
