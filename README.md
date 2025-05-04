# SnowPro
Выпускная работа C# Porfessional.Otus.Preview

Тема: "Ski school management system" ("Управление горнолыжной школой")

#### Описание
SnowPro - проект предназначен для записи на занятия, а также управления обучением в горнолыжной школе.
Проект был создан в рамках учебного курса OTUS "C# Developer. Professional" группа "C#-2024-09" команда 2.

#### Технологии
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- RabbitMQ
- Docker

#### Архитектура

Проект реализован на основе микросервисной архитектуры:
- Сервис авторизации/аутентификации
- Сервис уведомлений
- Сервис занятий
- Сервис профилей

#### Установка и запуск

1. Клонируйте репозиторий:

```
git clone https://github.com/otus-csharp-pro-sep2024-team2/SnowPro.git
```

2. Перейдите в директорию проекта:

```
cd SnowPro
```

3. Соберите и запустите проект с помощью Docker:

```
docker-compose up -d --build
```

4. Откройте в браузере:

	Swagger:
							
	- AuthorizationService [http://localhost:8084/swagger/index.html](http://localhost:8084/swagger/index.html)
	
	- LessonService [http://localhost:8085/swagger/index.html](http://localhost:8085/swagger/index.html)
	
	- ProfileService [http://localhost:8086/swagger/index.html](http://localhost:8086/swagger/index.html)
	
	- WebUi [http://localhost:3000]

   RabbitMQ:  [http://localhost:15672/](http://localhost:15672/)

   ngAdmin:   [http://localhost:5050/browser/](http://localhost:5050/browser/)

	4.1. Логин / Пароль для Postgres / RabitMq
   
	Login: `room2`
   
	Password: `room2Password`

	4.2. Вывод сервисов смотреть в Docker Desktop логах сервисов


#### Команда участников:

- Александр Ефимцев
- Ганиева Гузель
- Татьяна Басаргина
- Тузов Максим

Ментор: Олег Голенищев
