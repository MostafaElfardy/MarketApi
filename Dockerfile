# المرحلة 1: بناء ونشر المشروع
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MarketApi.csproj", "./"]
RUN dotnet restore "./MarketApi.csproj"
COPY . .
RUN dotnet publish "MarketApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# المرحلة 2: تشغيل التطبيق النهائي
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MarketApi.dll"]