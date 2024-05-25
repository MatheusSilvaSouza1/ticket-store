# Defina a imagem base para o ambiente de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7139

ENV ASPNETCORE_URLS=http://+:7139

# Defina a imagem para o ambiente de build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Copie o arquivo de projeto e restaure as dependências
COPY ./src/BuildingBlocks/Contracts/Contracts.csproj src/BuildingBlocks/Contracts/
COPY ./src/BuildingBlocks/Core/Core.csproj src/BuildingBlocks/Core/
COPY ./src/BuildingBlocks/MessageBus/MessageBus.csproj src/BuildingBlocks/MessageBus/
COPY ./src/Catalog/Catalog.API/API.csproj src/Catalog/Catalog.API/
COPY ./src/Catalog/Catalog.Application/Application.csproj src/Catalog/Catalog.Application/
COPY ./src/Catalog/Catalog.Domain/Domain.csproj src/Catalog/Catalog.Domain/
COPY ./src/Catalog/Catalog.Infra/Infra.csproj src/Catalog/Catalog.Infra/

RUN dotnet restore "src/Catalog/Catalog.API/API.csproj"

# Copie todo o restante do código
COPY . .

# Defina o diretório de trabalho e construa o projeto
WORKDIR "/src/src/Catalog/Catalog.API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Publique a aplicação
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Defina a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
