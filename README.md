# 💅 CosmetologySalon — система управления салоном красоты

<div align="center">

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![Blazor](https://img.shields.io/badge/Blazor-Server-brightgreen)
![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)
![Docker](https://img.shields.io/badge/Docker-✓-2496ED)
![License](https://img.shields.io/badge/License-MIT-green)

**Курсовой проект по дисциплине «Кроссплатформенная среда исполнения программного обеспечения»**

</div>

---

## 📖 О проекте

**CosmetologySalon** — это веб-приложение для автоматизации работы салона красоты.  
Система позволяет вести учёт клиентов, мастеров, услуг и записей на приём. Проект реализован на современном стеке .NET 8, Blazor Server и PostgreSQL.

Приложение демонстрирует навыки работы с:
- кроссплатформенной средой .NET;
- Entity Framework Core (Code First);
- Dependency Injection;
- Docker-контейнеризацией;
- построением интерактивных веб-интерфейсов на Blazor Server.

---

## ✨ Основные возможности

- 👥 **Управление клиентами** – добавление, редактирование, удаление и просмотр клиентов.
- 💇‍♀️ **Управление мастерами** – список мастеров с возможностью редактирования специализации.
- 📋 **Управление услугами** – каталог услуг салона (название, цена, длительность).
- 📅 **Записи на приём** – создание, перенос и отмена записей клиентов к мастерам.
- 🗂️ **Единая база данных** – все сущности связаны через EF Core.
- 🐳 **Полная контейнеризация** – запуск через Docker Compose без установки окружения на хост-машину.

---

## 🧰 Технологии

| Технология | Назначение |
|------------|------------|
| .NET 8 | Кроссплатформенная среда исполнения |
| ASP.NET Core | Веб-фреймворк |
| Blazor Server | Интерактивный веб-интерфейс на C# |
| Entity Framework Core 8 | ORM, Code First, миграции |
| PostgreSQL 16 | Реляционная база данных |
| FluentValidation (опционально) | Валидация моделей |
| Docker / Docker Compose | Контейнеризация и оркестрация |
| Git | Система контроля версий |

---

## 📁 Структура проекта
```
CosmetologySalon/
├── Components/ # Blazor-компоненты
│ └── ... (можно детализировать при необходимости)
├── Data/
│ └── SalonAppDbContext.cs # Контекст базы данных
├── DTOs/ # Объекты передачи данных
│ ├── AppointmentDTO.cs
│ ├── ClientDTO.cs
│ ├── MasterDTO.cs
│ └── ServiceDTO.cs
├── Migrations/ # Миграции EF Core
├── Models/ # Модели сущностей
│ ├── Appointment.cs
│ ├── Client.cs
│ ├── Master.cs
│ └── Service.cs
├── Pages/ # Страницы приложения (Blazor)
│ ├── Host.cshtml
│ ├── Appointments.razor
│ ├── Clients.razor
│ ├── Index.razor
│ ├── Masters.razor
│ └── Services.razor
├── Interfaces/ # Интерфейсы сервисов
│ ├── IAppointment.cs
│ ├── IClient.cs
│ ├── IMaster.cs
│ └── IService.cs
├── Services/ # Реализация бизнес-логики
│ ├── AppointmentService.cs
│ ├── ClientService.cs
│ ├── MasterService.cs
│ └── ServiceService.cs
├── Shared/ # Общие компоненты
│ └── MainLayout.razor
├── .gitignore
├── Imports.razor
├── App.razor
├── appsettings.json
├── docker-compose.yml
├── Dockerfile
├── Program.cs
├── README.md
└── Routes.razor
```

---

## 🚀 Быстрый старт

### Предварительные требования

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) — для локальной разработки
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) — для контейнеризации
- [PostgreSQL](https://www.postgresql.org/download/) — при локальном запуске
- [Git](https://git-scm.com/)

---

## 🐳 Запуск через Docker (рекомендуемый способ)

Не требует установки .NET SDK и PostgreSQL на хосте.

### Шаг 1: Клонирование репозитория
https://github.com/makaroshkina/CosmetologySalon.git

git clone https://github.com/makaroshkina/CosmetologySalon.git
cd CosmetologySalon

### Шаг 2: Запуск контейнеров
docker-compose up -d
### Шаг 3: Открыть приложение
http://localhost:8013
