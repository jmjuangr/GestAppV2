# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el código
COPY . .

# Restaurar todos los paquetes desde la solución
RUN dotnet restore GestApp.sln

# Compilar y publicar la API
RUN dotnet build GestApp.API/GestApp.API.csproj -c Release
RUN dotnet publish GestApp.API/GestApp.API.csproj -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8870
ENTRYPOINT ["dotnet", "GestApp.API.dll"]
