# Estágio 1: Construir a aplicação ASP.NET
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY TaskManager.csproj .
RUN dotnet restore
COPY . .
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --trust
RUN dotnet publish -c release -o /app

# Estágio 2: Imagem final para a aplicação ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "TaskManager.dll" ]