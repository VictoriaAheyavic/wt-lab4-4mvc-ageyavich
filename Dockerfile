# ============ СТАДИЯ 1: СБОРКА ============
# Берем официальный образ .NET SDK (там есть компилятор)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только файл проекта и восстанавливаем зависимости (кэшируется)
COPY *.csproj ./
RUN dotnet restore

# Копируем весь остальной код и собираем релизную версию
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ============ СТАДИЯ 2: ЗАПУСК ============
# Берем легкий образ без компилятора, только для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копируем готовые файлы из первой стадии
COPY --from=build /app/publish .

# Говорим, что приложение слушает порт 10000
EXPOSE 10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

# Команда запуска (имя DLL должно совпадать с именем вашего проекта!)
ENTRYPOINT ["dotnet", "wt-lab4-4mvc-ageyavich.dll"]