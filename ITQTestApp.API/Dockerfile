FROM node:20-alpine AS client-build
WORKDIR /src/itqtestapp.client
ENV VITE_SKIP_HTTPS=1
COPY itqtestapp.client/package*.json ./
RUN npm ci
COPY itqtestapp.client/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ITQTestApp.sln .
COPY ITQTestApp.API/ITQTestApp.API.csproj ITQTestApp.API/
COPY ITQTestApp.Application/ITQTestApp.Application.csproj ITQTestApp.Application/
COPY ITQTestApp.Domain/ITQTestApp.Domain.csproj ITQTestApp.Domain/
COPY ITQTestApp.Infrastructure/ITQTestApp.Infrastructure.csproj ITQTestApp.Infrastructure/
COPY itqtestapp.client/itqtestapp.client.esproj itqtestapp.client/
RUN dotnet restore ITQTestApp.API/ITQTestApp.API.csproj /p:SkipClientProject=true
COPY . .
RUN dotnet publish ITQTestApp.API/ITQTestApp.API.csproj -c Release -o /app/publish /p:SkipClientProject=true

COPY --from=client-build /src/itqtestapp.client/dist /app/publish/wwwroot

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "ITQTestApp.API.dll"]
