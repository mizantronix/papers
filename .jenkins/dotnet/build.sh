cd work

dotnet nuget remove source nuget.org
dotnet nuget add source -n internal http://192.168.68.104:8081/repository/nuget-group/index.json

dotnet restore -v n && dotnet build -o output