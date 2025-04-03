FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AnalizaEvaluarilor.APi/AnalizaEvaluarilor.APi.csproj", "AnalizaEvaluarilor.APi/"]
COPY ["AnalizaEvaluarilor.Application/AnalizaEvaluarilor.Application.csproj", "AnalizaEvaluarilor.Application/"]
COPY ["AnalizaEvaluarilor.Common/AnalizaEvaluarilor.Common.csproj", "AnalizaEvaluarilor.Common/"]
COPY ["AnalizaEvaluarilor.Infrastructure/AnalizaEvaluarilor.Infrastructure.csproj", "AnalizaEvaluarilor.Infrastructure/"]
COPY ["AnalizaEvaluarilor.Domain/AnalizaEvaluarilor.Domain.csproj", "AnalizaEvaluarilor.Domain/"]
RUN dotnet restore "AnalizaEvaluarilor.APi/AnalizaEvaluarilor.APi.csproj"
COPY . .
WORKDIR "/src/AnalizaEvaluarilor.APi"
RUN dotnet build "AnalizaEvaluarilor.APi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AnalizaEvaluarilor.APi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalizaEvaluarilor.APi.dll"]
