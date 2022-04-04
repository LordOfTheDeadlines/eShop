#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApps/Shop/ShopWebApp/ShopWebApp.csproj", "WebApps/Shop/ShopWebApp/"]
RUN dotnet restore "WebApps/Shop/ShopWebApp/ShopWebApp.csproj"
COPY . .
WORKDIR "/src/WebApps/Shop/ShopWebApp"
RUN dotnet build "ShopWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopWebApp.dll"]
