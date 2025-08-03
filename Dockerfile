# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY *.sln .
COPY src/ecommerce.api/*.csproj ./src/ecommerce.api/
COPY src/ecommerce.application/*.csproj ./src/ecommerce.application/
COPY src/ecommerce.Domain/*.csproj ./src/ecommerce.Domain/
COPY src/ecommerce.infra/*.csproj ./src/ecommerce.infra/

# Restore dependencies
RUN dotnet restore

# Copy the entire solution and publish
COPY . .
RUN dotnet publish src/ecommerce.api/ecommerce.api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY https/devcert.pfx https/devcert.pfx
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=7198
# Run the web project DLL
# Replace 'Web' with your actual web project name if different
ENTRYPOINT ["dotnet", "ecommerce.api.dll"]