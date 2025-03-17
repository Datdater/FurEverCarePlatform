# Use official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY *.sln ./
COPY FurEverCarePlatform.API/FurEverCarePlatform.API.csproj FurEverCarePlatform.API/
COPY FurEverCarePlatform.Application/FurEverCarePlatform.Application.csproj FurEverCarePlatform.Application/
COPY FurEverCarePlatform.Domain/FurEverCarePlatform.Domain.csproj FurEverCarePlatform.Domain/
COPY FurEverCarePlatform.Persistence/FurEverCarePlatform.Persistence.csproj FurEverCarePlatform.Persistence/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build the application
COPY . .
RUN dotnet publish FurEverCarePlatform.API/FurEverCarePlatform.API.csproj -c Release -o /out

# Use a runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FurEverCarePlatform.API.dll"]
