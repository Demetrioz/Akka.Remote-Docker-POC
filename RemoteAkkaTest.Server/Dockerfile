#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["RemoteAkkaTest.Server.csproj", "RemoteAkkaTest.Server/"]
RUN dotnet restore "RemoteAkkaTest.Server/RemoteAkkaTest.Server.csproj"

WORKDIR "/src/RemoteAkkaTest.Server"
COPY . .

RUN dotnet build "RemoteAkkaTest.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RemoteAkkaTest.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RemoteAkkaTest.Server.dll"]