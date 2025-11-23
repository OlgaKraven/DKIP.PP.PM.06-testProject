# ServiceDesk.Api — учебный REST API-сервис для Service Desk 

Учебный проект по разработке и сопровождению информационных систем на C# и ASP.NET Core Web API.  
Сервис реализует базовый REST API для учёта оборудования (Equipment) в контексте службы технической поддержки (Service Desk) и демонстрирует работу с:

- ASP.NET Core Web API;
- ORM Entity Framework Core;
- InMemory-базой данных для учебных/тестовых целей;
- автогенерацией Swagger/OpenAPI-документации.

> Проект может использоваться как стартовый шаблон для производственной практики по ПМ.06 Сопровождение информационных систем

---


## 1. Основные возможности

### Реализованные сущности:
- **Equipment** — учёт оборудования  
- **ServiceRequest** — заявки на обслуживание  
- **Health** — проверка работоспособности API

### Функциональность:
- Полный CRUD для Equipment  
- Полный CRUD для ServiceRequests  
- Проверка связи с БД  
- Валидация входных данных  
- Автодокументация API через Swagger  
- PostgreSQL + EF Core  
- Асинхронные операции (`async/await`)

---

## 2. Стек технологий

| Компонент | Используемые технологии |
|----------|--------------------------|
| Backend | C#, ASP.NET Core Web API |
| ORM | Entity Framework Core |
| База данных | PostgreSQL (Npgsql) |
| Документация | Swagger (Swashbuckle) |
| Средства разработки | .NET 9, Visual Studio / VS Code |

---

## 3. Структура проекта

```
ServiceDesk.Api/
│   Program.cs
│   ServiceDesk.Api.csproj
│   appsettings.json
│   appsettings.Development.json
│
├── Controllers/
│   ├── EquipmentController.cs
│   ├── ServiceRequestsController.cs
│   └── HealthController.cs
│
├── Data/
│   └── AppDbContext.cs
│
└── Models/
    ├── Equipment.cs
    └── ServiceRequest.cs
```

---

## 4. Модели

### Equipment
```csharp
public class Equipment
{
    public int Id { get; set; }
    public string InventoryNumber { get; set; } = "";
    public string Type { get; set; } = "";
    public string Room { get; set; } = "";
    public string Status { get; set; } = "";
}
```

### ServiceRequest
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

## 5. Конфигурация приложения

В `Program.cs`:

### Подключение PostgreSQL:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
```

### Swagger:
```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### Маршрутизация:
```csharp
app.MapControllers();
```

---

## 6. appsettings.json

Пустой, базовый:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

7. Настройки PostgreSQL находятся в **appsettings.Development.json**  
(в локальной сборке Visual Studio — это используемый файл).

---

## 8. Запуск проекта

### Через CLI:
```bash
cd ServiceDesk.Api
dotnet restore
dotnet run
```

API будет доступно по:
- `https://localhost:7xxx`
- `http://localhost:5xxx`

Swagger:
```
/swagger
```

---

## 9. REST API: перечень эндпоинтов

### Equipment (`/api/equipment`)
Метод | URL | Описание
------|-----|---------
GET | /api/equipment | Список оборудования
GET | /api/equipment/{id} | Получить по Id
POST | /api/equipment | Создать запись
PUT | /api/equipment/{id} | Обновить запись
DELETE | /api/equipment/{id} | Удалить запись

---

### ServiceRequests (`/api/servicerequests`)
Метод | URL | Описание
------|-----|---------
GET | /api/servicerequests | Список заявок
GET | /api/servicerequests/{id} | Получить по Id
POST | /api/servicerequests | Создать заявку
PUT | /api/servicerequests/{id} | Обновить заявку
DELETE | /api/servicerequests/{id} | Удалить заявку

---

### Health (`/health`)
Метод | URL | Описание
------|-----|---------
GET | /health | Проверка работы API

---

## 10. Пример ответа HealthController

```json
{
  "status": "Healthy"
}
```

---
