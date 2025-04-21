@echo off
docker-compose down 
rem -v
docker-compose build --no-cache
docker-compose up -d --remove-orphans
