FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY TaskManager.csproj .
RUN dotnet restore
COPY . .
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --trust
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "TaskManager.dll" ]

#comando para criar uma imagem -> docker build -t taskmanager .
#comando para executar um container com a imagem criada -> docker container run -it --rm -p 3000:80 --name taskmanagercontainer taskmanager

# FROM mcr.microsoft.com/mssql/server:2019-latest
# ENV SA_PASSWORD=SenhaSuperSegura123!
# ENV ACCEPT_EULA=Y
# COPY ./DbObjects/setup.sql /docker-entrypoint-initdb.d/
# EXPOSE 3004

#string de conexão Server=localhost,3004;Database=MeuBancoDeDados;User Id=taskmanagerdb;Password=SenhaSuperSegura123!;MultipleActiveResultSets=true
#comando para executar um container com a imagem do banco -> docker run -p 3004:1433 taskmanagercontainerdb
#docker build -t taskmanagercontainerdb

# docker-compose up