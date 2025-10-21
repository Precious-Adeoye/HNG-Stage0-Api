# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
# Use the .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj and restore dependencies
COPY ["HNG-Stage0-Api/HNG-Stage0-Api.csproj", "HNG-Stage0-Api/"]
RUN dotnet restore "HNG-Stage0-Api/HNG-Stage0-Api.csproj"

# Copy all source files and build
COPY . .
WORKDIR "/src/HNG-Stage0-Api"
RUN dotnet build "HNG-Stage0-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app (optimized for runtime)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HNG-Stage0-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# =============================
# ✅ Final runtime image
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set environment variables
ENV ConnectionStrings__DefaultConnection="Data Source=/app/app.db"
ENV ASPNETCORE_ENVIRONMENT="Production"

WORKDIR /app
COPY --from=publish /app/publish .

# Start the app
ENTRYPOINT ["dotnet", "HNG-Stage0-Api.dll"]
