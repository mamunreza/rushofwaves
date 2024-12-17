FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/Data/*.csproj ./Products/Data/
COPY Products/Domain/*.csproj ./Products/Domain/
COPY Products/EventConsumer/*.csproj ./Products/EventConsumer/
RUN dotnet restore Products/EventConsumer/EventConsumer.csproj

COPY Products/Data/. ./Products/Data/
COPY Products/Domain/. ./Products/Domain/
COPY Products/EventConsumer/. ./Products/EventConsumer/

RUN dotnet build Products/EventConsumer/EventConsumer.csproj -c Release -o /app/build
RUN dotnet publish Products/EventConsumer/EventConsumer.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8021

ENTRYPOINT ["dotnet", "EventConsumer.dll"]