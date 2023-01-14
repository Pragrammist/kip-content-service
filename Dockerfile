# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source


# copy csproj and restore as distinct layers

COPY src/ .
WORKDIR /source/Web
RUN dotnet restore

# copy everything else and build app

RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./



ENTRYPOINT ["dotnet", "Web.dll"]