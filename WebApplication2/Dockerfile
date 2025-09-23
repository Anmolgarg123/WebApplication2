# Use .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY WebApplication2/*.csproj ./WebApplication2/
COPY WebApplication2.Tests/*.csproj ./WebApplication2.Tests/
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build -c Release --no-restore
RUN dotnet publish WebApplication2 -c Release -o /out --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "WebApplication2.dll"]
