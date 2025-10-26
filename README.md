# Brigade.ru — Платформа для строительных бригад

[![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-10+-purple?logo=csharp)](https://learn.microsoft.com/ru-ru/dotnet/csharp/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15+-blue?logo=postgresql)](https://www.postgresql.org/)
[![JWT](https://img.shields.io/badge/JWT-Auth-orange)](https://jwt.io/)
[![SignalR](https://img.shields.io/badge/SignalR-Realtime-green?logo=signalr)](https://learn.microsoft.com/ru-ru/aspnet/core/signalr/)

> **Brigade.ru** — это веб-платформа для эффективного взаимодействия между заказчиками, строительными компаниями и исполнителями.  
> Проект реализован с использованием принципов **чистой архитектуры (Clean Architecture)** и ориентирован на масштабируемость, безопасность и высокую производительность.

---

## Архитектура проекта

Проект разделён на четыре основных слоя, каждый из которых отвечает за свою зону ответственности:
```bash
Brigade.sln
├── Brigade.Api/              # Web API, DI, контроллеры
├── Brigade.Application/      # Use cases, DTO, валидация
├── Brigade.Domain/           # Сущности, интерфейсы
├── Brigade.Infrastructure/   # EF Core, JWT, SignalR, репозитории
```
> Архитектура следует принципам **SOLID**, **Dependency Inversion**, **Separation of Concerns** и **Testability**.

---

## Используемые технологии

- **Язык**: C# 10+
- **Фреймворк**: .NET 9.0
- **ORM**: Entity Framework Core + PostgreSQL
- **Аутентификация**: JWT (JSON Web Tokens)
- **Realtime**: SignalR (**в будущем** чат между заказчиком и исполнителем)
- **DI**: Встроенный контейнер зависимостей ASP.NET Core
- **API Docs**: Swagger / OpenAPI
---

## Как запустить локально

### Предварительные требования

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- (Опционально) Docker + Docker Compose

### Шаги

1. **Клонируйте репозиторий**
   ```bash
   git clone https://github.com/SvenAventador/brigade--server.git
   cd brigade.ru

2. **Примените миграции (из папки Brigade.Api)**
    ```bash
    dotnet ef database update --project ../Brigade.Infrastructure

3. **Запустите API**
    ```bash
    dotnet run --project Brigade.Api

4. **Откройте документацию**\
    [Документация API из Swagger](http://localhost:5000/swagger)