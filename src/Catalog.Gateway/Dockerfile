#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Catalog.Gateway/Catalog.Gateway.csproj", "src/Catalog.Gateway/"]
RUN dotnet restore "src/Catalog.Gateway/Catalog.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Catalog.Gateway"
RUN dotnet build "Catalog.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Gateway.dll"]
