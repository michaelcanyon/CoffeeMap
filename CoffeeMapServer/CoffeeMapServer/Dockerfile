  # syntax=docker/dockerfile:1
  FROM mcr.microsoft.com/dotnet/aspnet:3.1
  COPY bin/Release/netcoreapp3.1/publish/ App/
  WORKDIR /App
  ENTRYPOINT ["dotnet", "CoffeeMapServer.dll"]