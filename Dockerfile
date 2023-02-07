# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
EXPOSE 80
WORKDIR /source

# copy csproj and restore as distinct layers
# COPY *.sln .
COPY TypeAheadApi/*.csproj ./TypeAheadApi/
COPY TypeAheadApi.Utils/*.csproj ./TypeAheadApi.Utils/
RUN dotnet restore ./TypeAheadApi.Utils/TypeAheadApi.Utils.csproj
RUN dotnet restore ./TypeAheadApi/TypeAheadApi.csproj

# copy everything else and build app
COPY TypeAheadApi/. ./TypeAheadApi/
COPY TypeAheadApi.Utils/. ./TypeAheadApi.Utils/
WORKDIR /source/TypeAheadApi
RUN dotnet build -c -o
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TypeAheadApi.dll"]