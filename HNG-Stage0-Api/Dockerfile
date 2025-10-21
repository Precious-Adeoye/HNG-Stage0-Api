# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["HNG-Stage0-Api/HNG-Stage0-Api.csproj", "HNG-Stage0-Api/"]
RUN dotnet restore "HNG-Stage0-Api/HNG-Stage0-Api.csproj"

# Copy the entire source and build
COPY . .
WORKDIR "/src/HNG-Stage0-Api"
RUN dotnet build "HNG-Stage0-Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- Publish Stage ---
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HNG-Stage0-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output
COPY --from=publish /app/publish .

# Environment variable for SQLite database path
ENV ConnectionStrings__DefaultConnection="Data Source=/app/app.db"

# Railway will provide the PORT environment variable
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 3000

 ENTRYPOINT ["dotnet", "HNG-Stage0-Api.dll"]


