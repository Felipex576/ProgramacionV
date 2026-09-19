# -------------------------------------------------------------
# Etapa 1: Base de ejecución (Runtime)
# -------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
# En .NET 8/9/10 el puerto HTTP por defecto es el 8080
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

# -------------------------------------------------------------
# Etapa 2: Compilación y Publicación (SDK)
# -------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos primero el archivo .csproj para aprovechar el caché de capas al restaurar paquetes
COPY ["programacionV.csproj", "./"]
RUN dotnet restore "programacionV.csproj"

# Copiamos todo el código fuente y publicamos los binarios optimizados
COPY . .
RUN dotnet publish "programacionV.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -------------------------------------------------------------
# Etapa 3: Imagen Final
# -------------------------------------------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "programacionV.dll"]