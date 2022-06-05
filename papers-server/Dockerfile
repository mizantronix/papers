FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG CONFIGURATION="Debug"
ARG VERSION="0.0.0.1"

WORKDIR /src
COPY . /src

RUN dotnet restore /src/Papers.sln
RUN mkdir -p /app
RUN dotnet build /src/Api/*csproj --configuration $CONFIGURATION -o /app -p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Papers.Api.dll"]