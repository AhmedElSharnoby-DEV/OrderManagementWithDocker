FROM mcr.microsoft.com/dotnet/sdk:9.0 AS Build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o /src/out
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /src
COPY --from=Build /src/out ./
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "INotificationManagementSystem.dll"]