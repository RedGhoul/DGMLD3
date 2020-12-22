
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DGMLD3.csproj", ""]
RUN dotnet restore "./DGMLD3.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DGMLD3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DGMLD3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DGMLD3.dll"]