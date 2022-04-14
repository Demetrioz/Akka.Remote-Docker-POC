# Akka.Remote-Docker-POC
Proof of concept for having a web API call an Actor system on a remote machine using docker-compose

## Getting started
1. First, run the following commands from the root directory:
```powershell
docker-compose build
docker-compose up
```
2. Use Postman to make a GET request to http://localhost:8000/test
You should receive a response similar to this:
```
{
    "result": {
        "message": "I handled the message RemoteAkkaTest.Server.TestMessage"
    },
    "id": 1,
    "exception": null,
    "status": 5,
    "isCanceled": false,
    "isCompleted": true,
    "isCompletedSuccessfully": true,
    "creationOptions": 64,
    "asyncState": null,
    "isFaulted": false
}
```
## Notes

- The server's public-hostname in akka config matches the service name in docker-compose
- To run outside of docker, change the server's public-hostname to localhost and the API test actor's ActorSelection from 
"akka.tcp://MyServer@server:8081/user/ServerTestActor" to "akka.tcp://MyServer@localhost:8081/user/ServerTestActor"

## TODO
- Get https requests working
