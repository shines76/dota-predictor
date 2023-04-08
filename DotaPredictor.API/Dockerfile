FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotaPredictor.API/DotaPredictor.API.csproj", "DotaPredictor.API/"]
RUN dotnet restore "DotaPredictor.API/DotaPredictor.API.csproj"
COPY . .
WORKDIR "/src/DotaPredictor.API"
RUN dotnet build "DotaPredictor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotaPredictor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotaPredictor.API.dll"]
