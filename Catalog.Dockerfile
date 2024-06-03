FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7139

ENV ASPNETCORE_URLS=http://+:7139

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["./src/BuildingBlocks/Contracts/Contracts.csproj"             , "src/BuildingBlocks/Contracts/"]
COPY ["./src/BuildingBlocks/Core/Core.csproj"                       , "src/BuildingBlocks/Core/"]
COPY ["./src/BuildingBlocks/MessageBus/MessageBus.csproj"           , "src/BuildingBlocks/MessageBus/"]
COPY ["./src/Catalog/Catalog.API/Catalog.API.csproj"                , "src/Catalog/Catalog.API/"]
COPY ["./src/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Catalog/Catalog.Application/"]
COPY ["./src/Catalog/Catalog.Domain/Catalog.Domain.csproj"          , "src/Catalog/Catalog.Domain/"]
COPY ["./src/Catalog/Catalog.Infra/Catalog.Infra.csproj"            , "src/Catalog/Catalog.Infra/"]

RUN dotnet restore "src/Catalog/Catalog.API/Catalog.API.csproj"

COPY . .

WORKDIR "/src/src/Catalog/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
