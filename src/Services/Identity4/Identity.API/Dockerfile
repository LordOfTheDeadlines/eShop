#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ARG source
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV certPassword pass@word

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity4/Identity.API/Identity.API.csproj", "Services/Identity4/Identity.API/"]
RUN dotnet restore "Services/Identity4/Identity.API/Identity.API.csproj"
#RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048
#RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key
#RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost'
#RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
#RUN openssl pkcs12 -export -out ./cert.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword}
COPY . .
WORKDIR "/src/Services/Identity4/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
