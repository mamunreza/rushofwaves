FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/EventPublisher/EventPublisher.csproj ./
# COPY . .
RUN dotnet restore

COPY Products/Domain/. ./
COPY Products/EventPublisher/. ./

RUN dotnet build EventPublisher.csproj -c Release -o /app/build
RUN dotnet publish EventPublisher.csproj -c Release -o /app/out
# RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
# COPY --from=build /app .
EXPOSE 8023

ENTRYPOINT ["dotnet", "EventPublisher.dll"]