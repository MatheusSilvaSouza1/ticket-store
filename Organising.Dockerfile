FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5286

ENV ASPNETCORE_URLS=http://+:5286

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["./src/BuildingBlocks/Contracts/Contracts.csproj"           , "src/BuildingBlocks/Contracts/"]
COPY ["./src/BuildingBlocks/Core/Core.csproj"                     , "src/BuildingBlocks/Core/"]
COPY ["./src/BuildingBlocks/MessageBus/MessageBus.csproj"         , "src/BuildingBlocks/MessageBus/"]
COPY ["./src/Organising/Organising.API/API.csproj"                , "src/Organising/Organising.API/"]
COPY ["./src/Organising/Organising.Application/Application.csproj", "src/Organising/Organising.Application/"]
COPY ["./src/Organising/Organising.Domain/Domain.csproj"          , "src/Organising/Organising.Domain/"]
COPY ["./src/Organising/Organising.Infra/Infra.csproj"            , "src/Organising/Organising.Infra/"]

RUN dotnet restore "src/Organising/Organising.API/API.csproj"

COPY . .

WORKDIR "/src/src/Organising/Organising.API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
