FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Frontend/WebForum.Frontend/WebForum.Frontend.csproj", "Src/Frontend/WebForum.Frontend/"]
RUN dotnet restore "Src/Frontend/WebForum.Frontend/WebForum.Frontend.csproj"
COPY . .
WORKDIR "/src/Src/Frontend/WebForum.Frontend"
RUN dotnet build "WebForum.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebForum.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebForum.Frontend.dll"]
