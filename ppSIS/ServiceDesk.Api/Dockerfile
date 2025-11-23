# Этап 1: базовый рантайм ASP.NET Core 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Приложение будет слушать на 8080 внутри контейнера
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Чтобы Swagger работал в контейнере, оставляем окружение Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Этап 2: SDK для сборки .NET 9
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем только csproj
COPY ["ServiceDesk.Api.csproj", "./"]

# Восстанавливаем зависимости
RUN dotnet restore "ServiceDesk.Api.csproj"

# Копируем остальной код
COPY . .

# Публикуем (Release)
RUN dotnet publish "ServiceDesk.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Этап 3: финальный образ (минимальный)
FROM base AS final
WORKDIR /app

# Копируем опубликованные файлы
COPY --from=build /app/publish .

# Точка входа
ENTRYPOINT ["dotnet", "ServiceDesk.Api.dll"]
