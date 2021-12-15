FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY WebTry.csproj WebTry.csproj
RUN dotnet restore WebTry.csproj
COPY . .
WORKDIR /src
RUN dotnet build "WebTry.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish WebTry.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebTry.dll"]