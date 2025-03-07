# Etapa de base para a aplica��o .NET
ARG BUILD_CONFIGURATION=Release
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-nanoserver-1809 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build da aplica��o .NET
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-nanoserver-1809 AS build
WORKDIR /src

# Copiar os arquivos do projeto (ajustar para o seu projeto espec�fico)
COPY ["EmployeeManagement.API/EmployeeManagement.API.csproj", "EmployeeManagement.API/"]
COPY ["EmployeeManagement.Application/EmployeeManagement.Application.csproj", "EmployeeManagement.Application/"]
COPY ["EmployeeManagement.Infrastructure/EmployeeManagement.Infrastructure.csproj", "EmployeeManagement.Infrastructure/"]
COPY ["EmployeeManagement.Domain/EmployeeManagement.Domain.csproj", "EmployeeManagement.Domain/"]

# Restaurar as depend�ncias
RUN dotnet restore "EmployeeManagement.API/EmployeeManagement.API.csproj"

# Copiar o restante dos arquivos
COPY . . 

# Construir o projeto
WORKDIR "/src/EmployeeManagement.API"
RUN dotnet build "EmployeeManagement.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

# Etapa de publica��o da aplica��o .NET
FROM build AS publish
RUN dotnet publish "EmployeeManagement.API.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

# Etapa final (imagem de execu��o) para a aplica��o .NET
FROM base AS final
WORKDIR /app

# Copiar os artefatos da etapa de publica��o para o container final
COPY --from=publish /app/publish .

# Definir o comando de entrada da aplica��o .NET
ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]

# Definindo a imagem e o cont�iner do SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest AS sqlserver

# Defina vari�veis de ambiente para o SQL Server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# Exponha a porta padr�o do SQL Server
EXPOSE 1433

# Inicie o SQL Server
CMD /opt/mssql/bin/sqlservr
