# NOTE: This is a single stage dockerfile which is used to gain access to dotnet ef tool to run migrations
# This is not intended for production use
FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

COPY *.sln .
COPY ExchangeRateUpdater.Api/*.csproj ExchangeRateUpdater.Api/
COPY ExchangeRateUpdater.Application/*.csproj ExchangeRateUpdater.Application/
COPY ExchangeRateUpdater.Domain/*.csproj ExchangeRateUpdater.Domain/
COPY ExchangeRateUpdater.Infrastructure/*.csproj ExchangeRateUpdater.Infrastructure/
COPY ExchangeRateUpdater.Tests/*.csproj ExchangeRateUpdater.Tests/

RUN dotnet restore

COPY . .
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app/publish --no-restore

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "/app/publish/ExchangeRateUpdater.Api.dll"]
