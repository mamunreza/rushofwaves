# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY Products/API/*.csproj ./Products/API/
COPY Products/Data/*.csproj ./Products/Data/
RUN dotnet restore Products/API/API.csproj

# Copy the rest of the application code
COPY Products/API/. ./Products/API/
COPY Products/Data/. ./Products/Data/

# Build the application
#WORKDIR /app/Products/API
RUN dotnet build Products/API/API.csproj -c Release -o /app/build
RUN dotnet publish Products/API/API.csproj -c Release -o /app/out
#RUN dotnet publish -c Release -o out

# Use the official .NET 8 runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./

# Copy the SQLite database
COPY Products/Data/app.db ./app.db

# Expose the port the application runs on
EXPOSE 8022
# Expose 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "API.dll"]