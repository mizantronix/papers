FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY Api/bin/Release/net6.0 /app
COPY appsettings.production.json /app/appsettings.json
ENTRYPOINT ["dotnet", "Papers.Api.dll"]