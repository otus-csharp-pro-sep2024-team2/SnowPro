# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Copy local NuGet packages
COPY ./nuget-packages/*.nupkg /nuget-packages/

#RUN ls -la /packages && exit 1
# Add local folder as NuGet source
#RUN dotnet nuget add source /packages --name local-packages

WORKDIR /src

ARG ROOT_DIR=NotificationSender/NotificationSmsSender
ARG PROJECT_NAME=NotificationSmsSender
ARG PROJECT_PATH=$PROJECT_NAME/$PROJECT_NAME.csproj

# Copy source code
COPY $ROOT_DIR/. .

# Restore dependencies
RUN dotnet restore $PROJECT_PATH -p:USE_DOCKER_NUGET=true

# Build and publish
RUN dotnet build $PROJECT_PATH -c Release -o /app/build
RUN dotnet publish $PROJECT_PATH -c Release -o /app/publish

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NotificationSmsSender.dll"]