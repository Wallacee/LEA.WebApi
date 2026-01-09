FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura usando o caminho correto
RUN dotnet restore LEA.WebApi.Web/LEA.WebApi.Web.csproj

# Build
RUN dotnet publish LEA.WebApi.Web/LEA.WebApi.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LEA.WebApi.Web.dll"]
