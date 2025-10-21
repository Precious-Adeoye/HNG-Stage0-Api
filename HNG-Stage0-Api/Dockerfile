# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["HNG-Stage0-Api/HNG-Stage0-Api.csproj", "HNG-Stage0-Api/"]
RUN dotnet restore "HNG-Stage0-Api/HNG-Stage0-Api.csproj"

COPY . .
WORKDIR "/src/HNG-Stage0-Api"
RUN dotnet publish "HNG-Stage0-Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy app
COPY --from=build /app/publish .

# SQLite database (inside container)
ENV ConnectionStrings__DefaultConnection="Data Source=/app/app.db"

# For hosted environments
ENV ASPNETCORE_URLS="http://0.0.0.0:${PORT}"
EXPOSE 8080

ENTRYPOINT ["dotnet", "HNG-Stage0-Api.dll"]
