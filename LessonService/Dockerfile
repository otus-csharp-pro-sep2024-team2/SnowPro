# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Create a folder for local NuGet packages
RUN mkdir -p /packages

# Copy local NuGet packages
COPY ./nuget-packages/*.nupkg /packages/
#RUN ls -la /packages && exit 1

# Add the local folder as a NuGet source
RUN dotnet nuget add source /packages --name local-packages

WORKDIR /src

ARG ROOT_DIR=LessonService
ARG PROJECT_NAME=LessonService.WebApi
ARG PROJECT_PATH=$ROOT_DIR/$PROJECT_NAME/$PROJECT_NAME.csproj

# Copy source code
COPY $ROOT_DIR/. $ROOT_DIR/.

# Restore dependencies
RUN dotnet restore $PROJECT_PATH -p:USE_DOCKER_NUGET=true  -v detailed

# Build and publish
RUN dotnet build $PROJECT_PATH -c Release -o /app/build
RUN dotnet publish $PROJECT_PATH -c Release -o /app/publish

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LessonService.WebApi.dll"]
