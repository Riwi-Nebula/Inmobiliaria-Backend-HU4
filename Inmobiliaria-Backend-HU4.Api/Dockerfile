FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inmobiliaria-Backend-HU4.Api/Inmobiliaria-Backend-HU4.Api.csproj", "Inmobiliaria-Backend-HU4.Api/"]
COPY ["Inmobiliaria-Backend-HU4.Domain/Inmobiliaria-Backend-HU4.Domain.csproj", "Inmobiliaria-Backend-HU4.Domain/"]
COPY ["Inmobiliaria-Backend-HU4.Application/Inmobiliaria-Backend-HU4.Application.csproj", "Inmobiliaria-Backend-HU4.Application/"]
COPY ["Inmobiliaria-Backend-HU4.Infrastructure/Inmobiliaria-Backend-HU4.Infrastructure.csproj", "Inmobiliaria-Backend-HU4.Infrastructure/"]
RUN dotnet restore "Inmobiliaria-Backend-HU4.Api/Inmobiliaria-Backend-HU4.Api.csproj"
COPY . .
WORKDIR "/src/Inmobiliaria-Backend-HU4.Api"
RUN dotnet build "./Inmobiliaria-Backend-HU4.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Inmobiliaria-Backend-HU4.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inmobiliaria-Backend-HU4.Api.dll"]
