# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies (Optimizes caching)
COPY ["GameStore.Api.csproj", "./"]
RUN dotnet restore "./GameStore.Api.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "GameStore.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "GameStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Environment variables for production
ENV ASPNETCORE_URLS=http://+:5140
ENV ASPNETCORE_ENVIRONMENT=Production

# Use a non-root user for security
USER $APP_UID

EXPOSE 5140

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
