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
   INSERT INTO auth_service."Roles" SELECT 1, 'Admin';
   INSERT INTO auth_service."Roles" SELECT 2, 'Client';
   INSERT INTO auth_service."Roles" SELECT 3, 'Instructor';

4. Выод сервисов смотреть в Docker Desktop логах сервисов

5. Token:
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNTJlMzgyMi1kMjdjLTRkNTctOTZiNi0zMDk2ODc2NTY0NmMiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzQ0OTc4NzAwLCJleHAiOjE3NDUwNjUxMDAsImlhdCI6MTc0NDk3ODcwMCwiaXNzIjoiQXV0aG9yaXphdGlvblNlcnZpY2UiLCJhdWQiOiJBdXRob3JpemF0aW9uU2VydmljZS5hcGkifQ.OUlIbB6b1rNhN3c5AjccWOEZmsGbfyBqPDbOfn8WxmI