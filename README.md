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

## HTTPS

For Https requests, complete the following:

1. Run the following commands

```
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\docker\dockerapp.pfx -p <PASSWORD>
dotnet dev-certs https --trust
```

2. Uncomment the following lines in docker-compose.

```
    # environment:
        # ASPNETCORE_URLS: https://+:443;http://+:80
        # ASPNETCORE_Kestrel__Certificates__Default__Password: <PASSWORD>
        # ASPNETCORE_Kestrel__Certificates__Default__Path: /https/dockerapp.pfx
    volumes:
      # - C:\Users\<USERNAME>\.aspnet\docker\https:/https/
```

3. Run the following commands

```powershell
docker-compose build
docker-compose up
```

4. Use Postman to make a GET request to https://localhost:8001/test
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
