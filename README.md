# SnowPro.Preview
Выпускная работа C# Porfessional.Otus.Preview

Тема: "Управление горнолыжной школой"

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
git clone https://github.com/sand721/SnowPro.Previw.git
```

2. Перейдите в директорию проекта:

```
cd SnowPro.Previw
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

   RabbitMQ:  [http://localhost:15672/](http://localhost:15672/)

   ngAdmin:   [http://localhost:5050/browser/](http://localhost:5050/browser/)

	4.1. Логин / Пароль для всех
   
	Login: `room2`
   
	Password: `room2Password`

	4.2. Добавьте хотябы одну роль в auth_service."Roles", база AuthorizationDb
   ```sql 
   INSERT INTO auth_service."Roles" SELECT 1, 'Admin';
   INSERT INTO auth_service."Roles" SELECT 2, 'Client';
   INSERT INTO auth_service."Roles" SELECT 3, 'Instructor';
   ```

	4.3. Вывод сервисов смотреть в Docker Desktop логах сервисов

	4.4. Token:
```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNTJlMzgyMi1kMjdjLTRkNTctOTZiNi0zMDk2ODc2NTY0NmMiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzQ1MjQ3MzU0LCJleHAiOjE3NDUzMzM3NTQsImlhdCI6MTc0NTI0NzM1NCwiaXNzIjoiQXV0aG9yaXphdGlvblNlcnZpY2UiLCJhdWQiOiJBdXRob3JpemF0aW9uU2VydmljZS5hcGkifQ.ooM3r8Gzbiv_4ruNO-FwZyfzRjVet-0Fhbrz9_AgmNI
```

#### Команда участников:

- Александр Ефимцев
- Ганиева Гузель
- Татьяна Басаргина
- Тузов Максим

Ментор: Олег Голенищев
