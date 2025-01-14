# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and project files
COPY ./MyMedicationApp.sln ./
COPY ./src/MyMedicationApp.Domain ./MyMedicationApp.Domain
COPY ./src/MyMedicationApp.Application ./MyMedicationApp.Application
COPY ./src/MyMedicationApp.Infrastructure ./MyMedicationApp.Infrastructure
COPY ./src/MyMedicationApp.Api ./MyMedicationApp.Api

# Restore dependencies
WORKDIR /app/MyMedicationApp.Api
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "MyMedicationApp.Api.dll"]
