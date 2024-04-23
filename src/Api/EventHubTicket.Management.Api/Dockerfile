#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/EventHubTicket.Management.Api/EventHubTicket.Management.Api.csproj", "src/Api/EventHubTicket.Management.Api/"]
COPY ["src/Core/EventHubTicket.Management.Application/EventHubTicket.Management.Application.csproj", "src/Core/EventHubTicket.Management.Application/"]
COPY ["src/Core/EventHubTicket.Management.Domain/EventHubTicket.Management.Domain.csproj", "src/Core/EventHubTicket.Management.Domain/"]
COPY ["src/Infrastructure/EventHubTicket.Management.Infrastructure/EventHubTicket.Management.Infrastructure.csproj", "src/Infrastructure/EventHubTicket.Management.Infrastructure/"]
COPY ["src/Infrastructure/EventHubTicket.Management.Persistence/EventHubTicket.Management.Persistence.csproj", "src/Infrastructure/EventHubTicket.Management.Persistence/"]
RUN dotnet restore "./src/Api/EventHubTicket.Management.Api/EventHubTicket.Management.Api.csproj"
COPY . .
WORKDIR "/src/src/Api/EventHubTicket.Management.Api"
RUN dotnet build "./EventHubTicket.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventHubTicket.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventHubTicket.Management.Api.dll"]