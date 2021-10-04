FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Foodoo.Data/Foodoo.Data.csproj", "Foodoo.Data/"]
RUN dotnet restore "Foodoo.Data/Foodoo.Data.csproj"
COPY . .
WORKDIR "/src/Foodoo.Data"
RUN dotnet build "Foodoo.Data.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foodoo.Data.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Foodoo.Data.dll"]
