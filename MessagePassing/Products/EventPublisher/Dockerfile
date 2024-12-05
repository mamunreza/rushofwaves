FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/EventPublisher/EventPublisher.csproj ./
# COPY . .
RUN dotnet restore

COPY Products/EventPublisher/. ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
# COPY --from=build /app .
EXPOSE 8012

ENTRYPOINT ["dotnet", "EventPublisher.dll"]