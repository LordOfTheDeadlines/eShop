#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Basket/Basket.Worker/Basket.Worker.csproj", "Services/Basket/Basket.Worker/"]
RUN dotnet restore "Services/Basket/Basket.Worker/Basket.Worker.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Basket.Worker"
RUN dotnet build "Basket.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Worker.dll"]
