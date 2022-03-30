#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Catalog.Worker/Catalog.Worker.csproj", "Services/Catalog/Catalog.Worker/"]
RUN dotnet restore "Services/Catalog/Catalog.Worker/Catalog.Worker.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Catalog.Worker"
RUN dotnet build "Catalog.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Worker.dll"]
