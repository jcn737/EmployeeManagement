# Etapa de base para a aplicação .NET
ARG BUILD_CONFIGURATION=Release
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-nanoserver-1809 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build da aplicação .NET
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-nanoserver-1809 AS build
WORKDIR /src

# Copiar os arquivos do projeto (ajustar para o seu projeto específico)
COPY ["EmployeeManagement.API/EmployeeManagement.API.csproj", "EmployeeManagement.API/"]
COPY ["EmployeeManagement.Application/EmployeeManagement.Application.csproj", "EmployeeManagement.Application/"]
COPY ["EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj", "EmployeeManagement.Infrastructure/"]
COPY ["EmployeeManagement.Domain/EmployeeManagement.Domain.csproj", "EmployeeManagement.Domain/"]

# Restaurar as dependências
RUN dotnet restore "EmployeeManagement.API/EmployeeManagement.API.csproj"

# Copiar o restante dos arquivos
COPY . . 

# Construir o projeto
WORKDIR "/src/EmployeeManagement.API"
RUN dotnet build "EmployeeManagement.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

# Etapa de publicação da aplicação .NET
FROM build AS publish
RUN dotnet publish "EmployeeManagement.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# Etapa final (imagem de execução) para a aplicação .NET
FROM base AS final
WORKDIR /app

# Copiar os artefatos da etapa de publicação para o container final
COPY --from=publish /app/publish .

# Definir o comando de entrada da aplicação .NET
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]

# Definindo a imagem e o contêiner do SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest AS sqlserver

# Defina variáveis de ambiente para o SQL Server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# Exponha a porta padrão do SQL Server
EXPOSE 1433

# Inicie o SQL Server
CMD /opt/mssql/bin/sqlservr
