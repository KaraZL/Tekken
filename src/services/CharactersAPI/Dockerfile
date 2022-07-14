from mcr.microsoft.com/dotnet/sdk:6.0 as publish
WORKDIR app/
copy src/services/CharactersAPI/*.csproj ./
RUN dotnet restore

copy src/services/CharactersAPI/ ./
RUN dotnet publish -c Release -o out/

from mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR app/
copy --from=publish app/out/ ./

EXPOSE 80

ENTRYPOINT ["dotnet", "CharactersAPI.dll"]
