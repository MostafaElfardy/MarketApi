FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MarketApi.csproj", "."]
RUN dotnet restore "./MarketApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MarketApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketApi.dll"]FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MarketApi.csproj", "."]
RUN dotnet restore "./MarketApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MarketApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketApi.dll"]