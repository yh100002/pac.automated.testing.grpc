﻿FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers

RUN dotnet restore "./POC.AutomatedTesting.gRPC.csproj"
# Build and publish a release
RUN dotnet publish "./POC.AutomatedTesting.gRPC.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "POC.AutomatedTesting.gRPC.dll"]