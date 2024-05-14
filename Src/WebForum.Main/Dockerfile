FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV NODE_VERSION=20.13.1
RUN apt install -y curl
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash
ENV NVM_DIR=/root/.nvm
RUN . "$NVM_DIR/nvm.sh" && nvm install ${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm use v${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm alias default v${NODE_VERSION}
ENV PATH="/root/.nvm/versions/node/v${NODE_VERSION}/bin/:${PATH}"

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/WebForum.Main/WebForum.Main.csproj", "Src/WebForum.Main/"]

COPY ["Src/Backend/Auth/WebForum.Auth.Api/WebForum.Auth.Api.csproj", "Src/Backend/Auth/WebForum.Auth.Api/"]
COPY ["Src/Backend/Auth/WebForum.Auth.Infrastructure/WebForum.Auth.Infrastructure.csproj", "Src/Backend/Auth/WebForum.Auth.Infrastructure/"]
COPY ["Src/Backend/Auth/WebForum.Auth.Application/WebForum.Auth.Application.csproj", "Src/Backend/Auth/WebForum.Auth.Application/"]
COPY ["Src/Backend/Auth/WebForum.Auth.Domain/WebForum.Auth.Domain.csproj", "Src/Backend/Auth/WebForum.Auth.Domain/"]

COPY ["Src/Common/Common/Common.csproj", "Src/Common/Common/"]

COPY ["Src/Backend/Core/WebForum.Core.Api/WebForum.Core.Api.csproj", "Src/Backend/Core/WebForum.Core.Api/"]
COPY ["Src/Backend/Core/WebForum.Core.Application/WebForum.Core.Application.csproj", "Src/Backend/Core/WebForum.Core.Application/"]
COPY ["Src/Backend/Core/WebForum.Core.Domain/WebForum.Core.Domain.csproj", "Src/Backend/Core/WebForum.Core.Domain/"]
COPY ["Src/Backend/Core/WebForum.Core.Infrastructure/WebForum.Core.Infrastructure.csproj", "Src/Backend/Core/WebForum.Core.Infrastructure/"]

COPY ["Src/Frontend/WebForum.Frontend/WebForum.Frontend.csproj", "Src/Frontend/WebForum.Frontend/"]

RUN dotnet restore "Src/WebForum.Main/WebForum.Main.csproj"
COPY . .

WORKDIR /src/Src/Frontend/WebForum.Frontend
RUN npm install

WORKDIR "/src/Src/WebForum.Main"
RUN dotnet build "WebForum.Main.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebForum.Main.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebForum.Main.dll"]
