# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeAccounting/EmployeeAccounting.csproj", "EmployeeAccounting/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DB/DB.csproj", "DB/"]
COPY ["Entities/Entities.csproj", "Entities/"]
RUN dotnet restore "./EmployeeAccounting/EmployeeAccounting.csproj"
COPY . .
WORKDIR "/src/EmployeeAccounting"
RUN dotnet build "./EmployeeAccounting.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeAccounting.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeAccounting.dll"]