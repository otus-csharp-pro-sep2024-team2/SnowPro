# SnowPro.Preview
Выпускная работа C# Porfessional. Otus. Preview

1. построить и запустить docker conteiner: docker-compose up -d --build
   Swagger:   http://localhost:8084/swagger/index.html
   RabbitMQ:  http://localhost:15672/
   ngAdmin:   http://localhost:5050/browser/

2. Логин / Пароль для всех
   Login: room2
   Password: room2Password

3. добавить хотябы одну роль в auth_service."Roles", база AuthorizationDb
   INSERT INTO auth_service."Roles" SELECT 1, 'Admin'

4. Выод сервисов смотреть в Docker Desktop логах сервисов