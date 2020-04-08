FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
ADD . /APP
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM ubuntu:19.10 AS build
WORKDIR /src
COPY ["gorila-cdb.csproj", "./"]
RUN apt-get update && apt-get install -y wget && rm -rf /var/lib/apt/lists/*
RUN wget https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN apt-get update && dpkg -i packages-microsoft-prod.deb && apt-get install apt-transport-https
RUN apt-get update && apt-get install dotnet-sdk-3.1 -y
RUN apt-get update && dotnet restore "./gorila-cdb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "gorila-cdb.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get install npm -y && npm i npm@latest -g
RUN dotnet publish "gorila-cdb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gorila-cdb.dll"]
