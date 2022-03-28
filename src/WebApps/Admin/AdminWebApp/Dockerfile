#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApps/Admin/AdminWebApp/AdminWebApp.csproj", "WebApps/Admin/AdminWebApp/"]
RUN dotnet restore "WebApps/Admin/AdminWebApp/AdminWebApp.csproj"
COPY . .
WORKDIR "/src/WebApps/Admin/AdminWebApp"
RUN dotnet build "AdminWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminWebApp.dll"]
