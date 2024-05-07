# .NET клиент для API Яндекс 360 для бизнеса

[![package](https://img.shields.io/nuget/vpre/Yandex.API360.svg?label=Yandex.API360&style=flat-square)](https://www.nuget.org/packages/Yandex.API360)
[![Bot API Version](https://img.shields.io/badge/Яндекс%20API-Март,%202024-f36caf.svg?style=flat-square)](https://yandex.ru/dev/api360/doc/concepts/versions.html#march-2024)

## Описание:

Главный класс библиотеки **Yandex.API360.Client**. В нем находятся все методы REST API Яндекс 360 для бизнеса. 

Для начала работы нужно создать новый экземпляр класса через конструктор, принимающий параметр типа **Yandex.API360.Api360Options**. После этого можно бутет вызывать все методы.

    public Client(Api360Options options)

Конструктор Yandex.API360.Api360Options принимает параметры organizationId и token
    
    public Api360Options(string organizationId, string token, string? baseUrl = default)

+ organizationId - Задает идентификатор организации в Яндекс 360 для бизнеса.
+ Задает токен авторизации в API Яндекс 360. Для получения токена ознакомьтесь с официальной документацией Яндекса.
