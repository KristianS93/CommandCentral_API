# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY CommandCentralApi.sln .
COPY Core/ ./Core/
COPY Infrastructure/ ./Infrastructure/
COPY WebApi/API/ ./WebApi/API/
RUN dotnet restore

# Build the projects
RUN dotnet build -c Release

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "API.dll"]