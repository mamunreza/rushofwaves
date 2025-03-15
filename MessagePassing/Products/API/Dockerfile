FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/Data/*.csproj ./Products/Data/
COPY Products/Domain/*.csproj ./Products/Domain/
COPY Products/API/*.csproj ./Products/API/
RUN dotnet restore Products/API/API.csproj

COPY Products/Data/. ./Products/Data/
COPY Products/Domain/. ./Products/Domain/
COPY Products/API/. ./Products/API/

RUN dotnet build Products/API/API.csproj -c Release -o /app/build
RUN dotnet publish Products/API/API.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8022
ENTRYPOINT ["sh", "-c", "sleep 30 && dotnet API.dll"]