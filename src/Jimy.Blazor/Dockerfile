FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Jimy.Blazor/Jimy.Blazor.csproj", "src/Jimy.Blazor/"]
RUN dotnet restore "src/Jimy.Blazor/Jimy.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Jimy.Blazor"
RUN dotnet build "Jimy.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Jimy.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/Jimy.Blazor/nginx.conf /etc/nginx/nginx.conf
EXPOSE 8080
