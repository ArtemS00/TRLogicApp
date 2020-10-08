Тестовое задание TRLogic.

+ Возможность загружать несколько файлов.
+ Возможность принимать multipart/form-data запросы.
+ Возможность принимать JSON запросы с BASE64 закодированными изображениями.
+ Возможность загружать изображения по заданному URL (изображение размещено где-то в интернете).
+ Создание квадратного превью изображения размером 100px на 100px.
+ Наличие модульных/интеграционных тестов.

+ Корректное завершение приложения при получении сигнала ОС (graceful shutdown).
- Dockerfile и docker-compose.yml, которые позволяют поднять приложение единой docker-compose up командой.
+ CI интеграция (Travis CI, Circle CI, другие).


Post API (for JSON): localhost:{port}/api/images
Post API (for form-data): localhost:{port}/api/images/form

Get API (for all): localhost:{port}/api/images
Get API (for one by id): localhost:{port}/api/images/{id}

IISExpress port - 50771.


Example of JSON request body:
[{
    "base64data":"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"
},
{
    "url":"https://static.toiimg.com/photo/72975551.cms"
}]


P.S. создал небольшое SPA на Angular для удобного тестирования (папка SPA, команда запуска: ng serve -o).
