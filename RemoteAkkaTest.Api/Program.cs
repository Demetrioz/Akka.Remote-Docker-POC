// https://getakka.net/articles/remoting/index.html
// https://github.com/akkadotnet/akka.net/blob/dev/src/examples/Chat/ChatClient/Program.cs

using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using RemoteAkkaTest.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(provider =>
{
    var config = ConfigurationFactory.ParseString(@"
    akka {  
        actor {
            provider = remote
        }
        remote {
            dot-netty.tcp {
		        port = 8080
		        hostname = localhost
            }
        }
    }
    ");

    var bootstrap = BootstrapSetup.Create().WithConfig(config);
    var di = DependencyResolverSetup.Create(provider);
    var actorSystemSetup = bootstrap.And(di);
    return ActorSystem.Create("AspNetDemo", actorSystemSetup);
});

builder.Services.AddSingleton(provider =>
{
    var actorSystem = provider.GetService<ActorSystem>();
    return actorSystem.ActorOf(Props.Create(() => new TestActor()), "ClientTestActor");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
