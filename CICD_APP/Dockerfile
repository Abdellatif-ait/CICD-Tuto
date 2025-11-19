# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_USER
ARG NUGET_PASSWORD
ARG NUGET_SOURCE_URL

WORKDIR /src

# Copy the project file (because it's in the same folder as the Dockerfile)
COPY ["CICD_APP.csproj", "./"]

# Configure Artifactory source
RUN dotnet nuget remove source Artifactory || true
RUN dotnet nuget add source "$NUGET_SOURCE_URL" \
    --name Artifactory \
    --username "$NUGET_USER" \
    --password "$NUGET_PASSWORD" \
    --store-password-in-clear-text

# Restore dependencies
RUN dotnet restore "CICD_APP.csproj"

# Copy all source files
COPY . .

# Build
RUN dotnet build "CICD_APP.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CICD_APP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final container image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CICD_APP.dll"]
