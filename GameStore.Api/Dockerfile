FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["GameStore.Api.csproj", "./"]
RUN dotnet restore "./GameStore.Api.csproj"

COPY . .
RUN dotnet build "GameStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5140
ENV ASPNETCORE_ENVIRONMENT=Production

USER $APP_UID

EXPOSE 5140

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
