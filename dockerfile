FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM  node:12-slim AS client 
RUN apt-get update \
    && apt-get install -y wget gnupg \
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf libxss1 \
      --no-install-recommends \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app 
COPY QReduction.Api/package.json . 
COPY QReduction.Api/package-lock.json . 
COPY QReduction.Api/html-to-pdf.js . 
COPY QReduction.Api/qr-code.js . 
RUN npm install 
RUN npm i puppeteer \
    # Add user so we don't need --no-sandbox.
    # same layer as npm install to keep re-chowned files from using up several hundred MBs more space
    && groupadd -r pptruser && useradd -r -g pptruser -G audio,video pptruser \
    && mkdir -p /home/pptruser/Downloads \
    && chown -R pptruser:pptruser /home/pptruser \
    && chown -R pptruser:pptruser /node_modules

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
RUN apt-get -y install curl
RUN curl -sL https://deb.nodesource.com/setup_12.x  | bash -
RUN apt-get -y install nodejs

RUN apt-get update \
    && apt-get install -y wget gnupg \
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf libxss1 \
      --no-install-recommends \
    && rm -rf /var/lib/apt/lists/*

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
COPY --from=client /app/ /app/
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QReduction.Api.dll"]
