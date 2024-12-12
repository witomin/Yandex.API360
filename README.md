# .NET клиент для API Яндекс 360 для бизнеса

[![NuGet Version](https://img.shields.io/nuget/vpre/Yandex.API360.svg?label=Yandex.API360&style=flat-square)](https://www.nuget.org/packages/Yandex.API360)
[![NuGet](https://img.shields.io/nuget/dt/Yandex.API360.svg)](https://www.nuget.org/packages/Yandex.API360) 
[![API Version](https://img.shields.io/badge/Яндекс%20API-Сентябрь,%202024-f36caf.svg?style=flat-square)](https://yandex.ru/dev/api360/doc/ru/versions#september-2024)

## Описание:

Главный класс библиотеки **Yandex.API360.Client**. В нем находятся все методы REST API Яндекс 360 для бизнеса. 
Методы сгруппированы в зависимости от своего назанчения:

+ группа Mailboxes - Операции с общими ящиками
+ группа TwoFA - Операции с 2FA
+ группа Antispam - Операции с настройками антиспама
+ группа Audit - Операции с аудитом
+ группа AuthSettings - Операции с настройками аутентификации
+ группа Departments - Операции с подразделениями
+ группа DNS - Операции с DNS
+ группа Domains - Операции с доменами
+ группа Groups - Операции с группами
+ группа Organization - Операции с организациями
+ группа PasswordManagement - Управление паролями
+ группа PostSettings - Управление настройками почты сотрудников
+ группа Routing - Управление правилами обработки писем
+ группа Users - Управление пользователями

Для начала работы нужно создать новый экземпляр класса через конструктор, принимающий параметр типа **Yandex.API360.Api360Options**. После этого можно бутет вызывать все методы.

    ```csharp
    public Client(Api360Options options)

Конструктор Yandex.API360.Api360Options принимает параметры organizationId и token
    
    ```csharp
    public Api360Options(string organizationId, string token, string? baseUrl = default)

+ organizationId - Задает идентификатор организации в Яндекс 360 для бизнеса.
+ Задает токен авторизации в API Яндекс 360. Для получения токена ознакомьтесь с официальной документацией Яндекса.

## Пример:

    ```csharp
    var APIClient = new Yandex.API360.Client(new Yandex.API360.Api360Options("OrganizationId", "Token"));
    var AllUsers = await APIClient.Users.GetListAllAsync();
