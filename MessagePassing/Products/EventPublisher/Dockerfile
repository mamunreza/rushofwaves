FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/Data/*.csproj ./Products/Data/
COPY Products/Domain/*.csproj ./Products/Domain/
COPY Products/EventPublisher/*.csproj ./Products/EventPublisher/
RUN dotnet restore Products/EventPublisher/EventPublisher.csproj

COPY Products/Data/. ./Products/Data/
COPY Products/Domain/. ./Products/Domain/
COPY Products/EventPublisher/. ./Products/EventPublisher/

RUN dotnet build Products/EventPublisher/EventPublisher.csproj -c Release -o /app/build
RUN dotnet publish Products/EventPublisher/EventPublisher.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8023

ENTRYPOINT ["sh", "-c", "sleep 30 && dotnet EventPublisher.dll"]