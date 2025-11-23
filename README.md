# ServiceDesk.Api — учебный REST API-сервис для Service Desk 

Учебный проект по разработке и сопровождению информационных систем на C# и ASP.NET Core Web API.  
Сервис реализует базовый REST API для учёта оборудования (Equipment) в контексте службы технической поддержки (Service Desk) и демонстрирует работу с:

- ASP.NET Core Web API;
- ORM Entity Framework Core;
- InMemory-базой данных для учебных/тестовых целей;
- автогенерацией Swagger/OpenAPI-документации.

> Проект может использоваться как стартовый шаблон для производственной практики по разработке и сопровождению веб-сервисов и информационных систем.

---

## 1. Стек технологий

- **Язык:** C#
- **Платформа:** .NET **9.0** (`net9.0`)
- **Web-фреймворк:** ASP.NET Core Web API
- **ORM:** Entity Framework Core 8
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.InMemory`
- **Документация API:** Swashbuckle / Swagger (`Swashbuckle.AspNetCore`)

---

## 2. Структура проекта

```text
ServiceDesk.Api/
│   Program.cs                 # Точка входа, настройка сервисов и middleware
│   ServiceDesk.Api.csproj    # Файл проекта (.NET 9, подключения пакетов)
│   appsettings.json          # Общие настройки логирования и хостинга
│   appsettings.Development.json
│
├── Controllers/
│   └── EquipmentController.cs # REST-контроллер для сущности Equipment
│
├── Data/
│   └── AppDbContext.cs       # Класс контекста EF Core (InMemory БД)
│
└── Models/
    ├── Equipment.cs          # Модель оборудования
    └── ServiceRequest.cs     # Модель заявки на обслуживание (пока без контроллера)
```

### 2.1. Модель Equipment
```csharp
public class Equipment
{
    public int Id { get; set; }
    public string InventoryNumber { get; set; } = "";
    public string Type { get; set; } = "";
    public string Room { get; set; } = "";
    public string Status { get; set; } = "Работает";
}
```

### 2.2. Модель ServiceRequest
```csharp
public class ServiceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Priority { get; set; } = "Средний";
    public string Status { get; set; } = "Новая";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }

    public int? EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
}
```

---

## 3. Конфигурация приложения

Основная конфигурация задаётся в `Program.cs`.

**InMemory DB:**
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ServiceDeskDb"));
```

**Swagger:**
```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

---

## 4. Требования к окружению

- .NET SDK 9.0  
- Любая IDE: VS 2022, Rider, VS Code  
- Git (опционально)

---

## 5. Запуск

### CLI
```bash
cd ServiceDesk.Api
dotnet restore
dotnet run
```

Swagger:  
`http://localhost:5xxx/swagger`

---

## 6. REST API: Equipment

База:  
`/api/equipment`

### Примеры:

GET список:
```json
[
  {
    "id": 1,
    "inventoryNumber": "PC-001",
    "type": "ПК",
    "room": "101",
    "status": "Работает"
  }
]
```

POST создание:
```json
{
  "inventoryNumber": "PRN-010",
  "type": "Принтер",
  "room": "202",
  "status": "Работает"
}
```

PUT обновление:
```json
{
  "id": 1,
  "inventoryNumber": "PC-001",
  "type": "ПК",
  "room": "101",
  "status": "Не работает"
}
```

---

## 7. Swagger / OpenAPI

Доступен по `/swagger`.

---

## 8. Возможные улучшения

- CRUD для ServiceRequest  
- Подключение реальной БД  
- Миграции EF  
- Dockerfile + docker-compose  
- Авторизация и роли  
- Логирование  

---

## 9. Итоги

Проект подходит для:

- изучения REST API;
- демонстрации EF Core;
- проведения учебной практики по сопровождению ИС;
- расширения до реального Service Desk приложения.
