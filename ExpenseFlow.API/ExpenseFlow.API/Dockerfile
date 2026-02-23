# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ExpenseFlow.API.csproj .
RUN dotnet restore ExpenseFlow.API.csproj

# Copy everything else
COPY . .
RUN dotnet publish ExpenseFlow.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ExpenseFlow.API.dll"]