# ==========================
# STAGE 1 — BUILD
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY *.sln ./

COPY LEA.WebApi.Web/*.csproj LEA.WebApi.Web/
COPY LEA.WebApi.Dal/*.csproj LEA.WebApi.Dal/
COPY LEA.WebApi.IoC/*.csproj LEA.WebApi.IoC/
COPY LEA.WebApi.Service/*.csproj LEA.WebApi.Service/
COPY LEA.WebApi.Domain/*.csproj LEA.WebApi.Domain/
COPY LEA.WebApi.Auth/*.csproj LEA.WebApi.Auth/
COPY LEA.WebApi.Infra/*.csproj LEA.WebApi.Infra/


RUN dotnet restore

# Copia tudo
COPY . .
WORKDIR /src/LEA.WebApi.Web
RUN dotnet publish -c Release -o /app/publish

# ==========================
# STAGE 2 — RUNTIME
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "LEA.WebApi.Web.dll"]
