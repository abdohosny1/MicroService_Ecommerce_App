#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Ordring/OrderApi/OrderApi.csproj", "src/Ordring/OrderApi/"]
COPY ["src/Ordring/OrderingApplication/OrderingApplication.csproj", "src/Ordring/OrderingApplication/"]
COPY ["src/Ordring/OrderingCore/OrderingCore.csproj", "src/Ordring/OrderingCore/"]
COPY ["src/Ordring/OrderingInfrastructure/OrderingInfrastructure.csproj", "src/Ordring/OrderingInfrastructure/"]
COPY ["src/common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/common/EventBusRabbitMQ/"]
RUN dotnet restore "src/Ordring/OrderApi/OrderApi.csproj"
COPY . .
WORKDIR "/src/src/Ordring/OrderApi"
RUN dotnet build "OrderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderApi.dll"]
