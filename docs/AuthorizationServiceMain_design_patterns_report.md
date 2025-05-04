
# Отчёт о шаблонах проектирования в проекте AuthorizationServiceMain

---

## 🔧 Использованные шаблоны проектирования

### 📂 Контроллер (`AuthController.cs`)

| Шаблон                 | Описание                                                                 |
|------------------------|--------------------------------------------------------------------------|
| **Dependency Injection** | Внедрение `IUserService`, `ILogger`, `IMapper`, `IMessageService` через конструктор |
| **MVC (Controller)**     | Контроллер в рамках ASP.NET Core MVC                                    |
| **Mapper**               | Использование `AutoMapper` для преобразования моделей и DTO             |
| **Logging**              | Внедрение `ILogger` для ведения журналов                                |

### 🧩 Сервисы (`UserService.cs`, `TokenService.cs`, `TokenCleanupService.cs`)

| Шаблон                 | Описание                                                                 |
|------------------------|--------------------------------------------------------------------------|
| **Service Layer**       | Реализация бизнес-логики отдельно от контроллера                        |
| **Dependency Injection**| Внедрение `DbContext`, `IConfiguration`, `ITokenService`                 |
| **Repository-like Access** | Прямой доступ к `DbContext` (без отдельного репозитория)              |
| **Strategy (Hashing)**  | Использование `BCrypt` как стратегия хеширования паролей                 |
| **Factory Method**      | Создание JWT через `JwtSecurityTokenHandler.CreateToken(...)`            |
| **Configuration Settings** | Использование `IConfiguration` для чтения параметров (JWT, и т.д.)     |

### 🔄 Маппинг (`AuthMapping.cs`)

| Шаблон          | Описание                                                                 |
|------------------|--------------------------------------------------------------------------|
| **Mapper**        | Настройка преобразования объектов с помощью AutoMapper (`CreateMap<...>`) |
| **Profile**       | Класс `AuthMapping` наследует `AutoMapper.Profile`                      |

---

## 🏗 Архитектурные принципы

| Подход                   | Применение                                                                 |
|--------------------------|----------------------------------------------------------------------------|
| **Layered Architecture** | Проект разбит на слои: API, Application, Domain, Infrastructure            |
| **SOLID-принципы**       | Использование интерфейсов, DI, разделение ответственности                 |
| **Separation of Concerns** | Контроллеры, сервисы, мапперы и модели разделены по слоям                   |
