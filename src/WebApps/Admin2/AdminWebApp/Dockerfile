#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN openssl genrsa -des3 -passout pass:pass -out server.key 2048
RUN openssl rsa -passin pass:pass -in server.key -out server.key
RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost'
RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
RUN openssl pkcs12 -export -out ./cerf.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:pass
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApps/Admin2/AdminWebApp/AdminWebApp.csproj", "WebApps/Admin2/AdminWebApp/"]
RUN dotnet restore "WebApps/Admin2/AdminWebApp/AdminWebApp.csproj"
COPY . .
WORKDIR "/src/WebApps/Admin2/AdminWebApp"
RUN dotnet build "AdminWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminWebApp.dll"]
