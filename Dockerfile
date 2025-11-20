# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar solo el proyecto
COPY ["EjemploRailway.csproj", "./"]
RUN dotnet restore "./EjemploRailway.csproj"

# Copiar el resto
COPY . .
RUN dotnet build "EjemploRailway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EjemploRailway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["sh", "-c", "dotnet EjemploRailway.dll --urls http://*:${PORT:-8080}"]
