FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /ap

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
RUN dotnet restore
WORKDIR /src/YouYou.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YouYou.Api.dll"]