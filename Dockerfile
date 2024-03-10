# Stage 1: Build the ASP.NET application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY TaskManager.csproj .
RUN dotnet restore
COPY . .
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https --trust

COPY setup_env.sh .
RUN chmod +x setup_env.sh
RUN ./setup_env.sh

RUN dotnet publish -c release -o /app

# Stage 2: Final image for the ASP.NET application
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "TaskManager.dll" ]