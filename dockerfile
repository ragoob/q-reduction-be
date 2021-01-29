FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

RUN ["apt-get", "update"]
RUN ["apt-get", "-y", "install", "libgdiplus"]
RUN ["apt-get", "-y", "install", "xvfb", "libfontconfig", "wkhtmltopdf"]
RUN ["apt-get", "-y", "install", "libc6-dev"]
RUN ["apt-get", "-y", "install", "openssl"]
RUN ["apt-get", "-y", "install", "libssl1.0-dev"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build


RUN ["apt-get", "update"]
RUN ["apt-get", "-y", "install", "libgdiplus"]
RUN ["apt-get", "-y", "install", "xvfb", "libfontconfig", "wkhtmltopdf"]
RUN ["apt-get", "-y", "install", "libc6-dev"]
RUN ["apt-get", "-y", "install", "openssl"]
RUN ["apt-get", "-y", "install", "libssl1.0-dev"]

WORKDIR /src
COPY ["QReduction.Api/QReduction.Api.csproj", "QReduction.Api/"]
COPY ["QReduction.Core/QReduction.Core.csproj", "QReduction.Core/"]
COPY ["QReduction.Infrastructure/QReduction.Infrastructure.csproj", "QReduction.Infrastructure/"]
COPY ["QReduction.Services/QReduction.Services.csproj", "QReduction.Services/"]
RUN dotnet restore "QReduction.Api/QReduction.Api.csproj"
COPY . .
WORKDIR "/src/QReduction.Api"


RUN dotnet build "QReduction.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QReduction.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
VOLUME /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QReduction.Api.dll"]
