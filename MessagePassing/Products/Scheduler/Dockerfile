FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Products/Data/*.csproj ./Products/Data/
COPY Products/Scheduler/*.csproj ./Products/Scheduler/
RUN dotnet restore Products/Scheduler/Scheduler.csproj

COPY Products/Data/. ./Products/Data/
COPY Products/Scheduler/. ./Products/Scheduler/

RUN dotnet build Products/Scheduler/Scheduler.csproj -c Release -o /app/build
RUN dotnet publish Products/Scheduler/Scheduler.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8024

ENTRYPOINT ["dotnet", "Scheduler.dll"]