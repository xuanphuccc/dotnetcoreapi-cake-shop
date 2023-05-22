# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./dotnetcoreapi-cake-shop.csproj" --disable-parallel
RUN dotnet publish "./dotnetcoreapi-cake-shop.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "dotnetcoreapi-cake-shop.dll"]