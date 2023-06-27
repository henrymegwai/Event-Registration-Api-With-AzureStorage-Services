#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Event-Registration-With-AzureStorageTable/Event-Registration-With-AzureStorageTable.csproj", "Event-Registration-With-AzureStorageTable/"]
RUN dotnet restore "Event-Registration-With-AzureStorageTable/Event-Registration-With-AzureStorageTable.csproj"
COPY . .
WORKDIR "/src/Event-Registration-With-AzureStorageTable"
RUN dotnet build "Event-Registration-With-AzureStorageTable.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Event-Registration-With-AzureStorageTable.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Event-Registration-With-AzureStorageTable.dll"]