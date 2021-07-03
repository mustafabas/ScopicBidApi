FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore "BidApp.WebApi.csproj"
WORKDIR "/src/."
COPY . .
RUN dotnet build "BidApp.WebApi.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "BidApp.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BidApp.WebApi.dll"]