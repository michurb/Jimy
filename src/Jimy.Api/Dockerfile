FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jimy.Api/Jimy.Api.csproj", "src/Jimy.Api/"]
COPY ["src/Jimy.Application/Jimy.Application.csproj", "src/Jimy.Application/"]
COPY ["src/Jimy.Core/Jimy.Core.csproj", "src/Jimy.Core/"]
COPY ["src/Jimy.Blazor/Jimy.Blazor.csproj", "src/Jimy.Blazor/"]
COPY ["src/Jimy.Infrastructure/Jimy.Infrastructure.csproj", "src/Jimy.Infrastructure/"]
RUN dotnet restore "src/Jimy.Api/Jimy.Api.csproj"
COPY . .
WORKDIR "/src/src/Jimy.Api"
RUN dotnet build "Jimy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Jimy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jimy.Api.dll"]
