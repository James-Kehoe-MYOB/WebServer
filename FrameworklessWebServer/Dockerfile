FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS Build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS API
COPY --from=Build /src/bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "FrameworklessWebServer.dll"]
