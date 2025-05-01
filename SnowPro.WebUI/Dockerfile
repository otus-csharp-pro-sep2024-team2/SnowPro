# Стадия 1: Сборка React-приложения
FROM node:18-alpine as builder

# Рабочая директория
WORKDIR /app

# Копируем package.json и устанавливаем зависимости
COPY SnowPro.WebUI/package.json SnowPro.WebUI/package-lock.json ./
RUN npm install

# Копируем весь исходный код и собираем проект
COPY SnowPro.WebUI/ ./
RUN npm run build

# Стадия 2: Настройка Nginx и копирование билда
FROM nginx:alpine

# Копируем билд из предыдущей стадии
COPY --from=builder /app/build /usr/share/nginx/html

# Копируем nginx-конфигурацию
COPY SnowPro.WebUI/nginx.conf /etc/nginx/conf.d/default.conf
