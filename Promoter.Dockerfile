FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5286

ENV ASPNETCORE_URLS=http://+:5286

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["./src/BuildingBlocks/Contracts/Contracts.csproj"                , "src/BuildingBlocks/Contracts/"]
COPY ["./src/BuildingBlocks/Core/Core.csproj"                          , "src/BuildingBlocks/Core/"]
COPY ["./src/BuildingBlocks/MessageBus/MessageBus.csproj"              , "src/BuildingBlocks/MessageBus/"]
COPY ["./src/Promoter/Promoter.API/Promoter.API.csproj"                , "src/Promoter/Promoter.API/"]
COPY ["./src/Promoter/Promoter.Application/Promoter.Application.csproj", "src/Promoter/Promoter.Application/"]
COPY ["./src/Promoter/Promoter.Domain/Promoter.Domain.csproj"          , "src/Promoter/Promoter.Domain/"]
COPY ["./src/Promoter/Promoter.Infra/Promoter.Infra.csproj"            , "src/Promoter/Promoter.Infra/"]

RUN dotnet restore "src/Promoter/Promoter.API/Promoter.API.csproj"

COPY . .

WORKDIR "/src/src/Promoter/Promoter.API"
RUN dotnet build "Promoter.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Promoter.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promoter.API.dll"]
