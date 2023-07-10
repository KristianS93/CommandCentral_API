FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY CommandCentralAPI.sln .
COPY Core/ ./Core/
COPY Infrastructure/ ./Infrastructure/
COPY WebApi/API/ ./WebApi/API/

COPY Infrastructure.Tests/Infrastructure.Tests.csproj ./Infrastructure.Tests/
COPY WebApi.Tests/WebApi.Tests.csproj ./WebApi.Tests/
RUN dotnet restore

# Build the projects
RUN dotnet build -c Release

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage
# Set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]