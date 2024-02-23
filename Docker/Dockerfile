FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
EXPOSE 80
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY ../ ./
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --trust
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "TaskManager.dll" ]

# FROM mcr.microsoft.com/mssql/server:latest
# ENV SA_PASSWORD=YourStrong@Passw0rd
# ENV ACCEPT_EULA=Y
# EXPOSE 3004

#docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" -p 3004:1433 --name taskmanager-sql -d mcr.microsoft.com/mssql/server:latest
#docker exec -it taskmanager-sql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd
#docker build -t taskmanager .
#docker run -d -p 3000:80 --name taskmanager-api taskmanager
#docker network create taskmanager-network
#docker network connect taskmanager-network taskmanager-api
#docker network connect taskmanager-network taskmanager-sql

#remover da rede ex: docker network disconnect taskmanager-network taskmanager-app
#deletar rede docker network rm taskmanager-network
