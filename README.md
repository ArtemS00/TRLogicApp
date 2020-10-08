### Тестовое задание TRLogic.

- [x] Возможность загружать несколько файлов.
- [x] Возможность принимать multipart/form-data запросы.
- [x] Возможность принимать JSON запросы с BASE64 закодированными изображениями.
- [x] Возможность загружать изображения по заданному URL (изображение размещено где-то в интернете).
- [x] Создание квадратного превью изображения размером 100px на 100px.
- [x] Наличие модульных/интеграционных тестов.

- [x] Корректное завершение приложения при получении сигнала ОС (graceful shutdown).
- [ ] Dockerfile и docker-compose.yml, которые позволяют поднять приложение единой docker-compose up командой.
- [x] CI интеграция (Travis CI, Circle CI, другие).


Post API (for JSON): localhost:{port}/api/images

Post API (for form-data): localhost:{port}/api/images/form


Get API (for all): localhost:{port}/api/images

Get API (for one by id): localhost:{port}/api/images/{id}

IISExpress port - 50771.


Example of JSON request body:
```json
[{
    "base64data":"{your image in base64 format}"
},
{
    "url":"{image url}"
}]
```

P.S. создал небольшое SPA на Angular для удобного тестирования (папка SPA, команда запуска: ng serve -o).
