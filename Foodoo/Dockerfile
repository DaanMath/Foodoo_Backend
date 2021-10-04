FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Foodoo/Foodoo.csproj", "Foodoo/"]
RUN dotnet restore "Foodoo/Foodoo.csproj"
COPY . .
WORKDIR "/src/Foodoo"
RUN dotnet build "Foodoo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foodoo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Foodoo.dll"]
