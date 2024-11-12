# BASE - Criação da estrutura de pastas base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN mkdir /api
WORKDIR /api
EXPOSE 8080

# PUBLISH - Publicação da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /api
COPY . .
RUN dotnet restore "AuthService.csproj"
RUN dotnet publish "AuthService.csproj" -c Release -o dist

# FINAL - Cópia dos arquivos do publish para a estrutura de pastas base
FROM base AS final
WORKDIR /dist
COPY --from=publish /api/dist .
ENTRYPOINT ["dotnet", "AuthService.dll"]