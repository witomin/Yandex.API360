# Yandex.API360
Библиотека для управления структурой организации Яндекс 360 для бизнеса с помощью REST API

## Описание:

Главный класс библиотеки **Yandex.API360.Client**. В нем находятся все методы REST API Яндекс 360 для бизнеса. 

Для начала работы нужно создать новый экземпляр класса через конструктор, принимающий параметр типа **Yandex.API360.Api360Options**. После этого можно бутет вызывать все методы.

    public Client(Api360Options options)

Конструктор Yandex.API360.Api360Options принимает параметры organizationId и token
    
    public Api360Options(string organizationId, string token, string? baseUrl = default)

+ organizationId - Задает идентификатор организации в Яндекс 360 для бизнеса.
+ Задает токен авторизации в API Яндекс 360. Для получения токена ознакомьтесь с официальной документацией Яндекса.

## Доступные методы:

### **GetUsersAsync(int page, int perPage)**
 Получить список сотрудников постранично
#### *Параметры:*
+ page Номер страницы ответа
+ perPageКоличество сотрудников на одной странице ответа

### **GetAllUsersAsync()** 
Получить полный списко сотрудников

### **GetUserByIdAsync(ulong userId)**
Получить сотрудника по Id
#### *Параметры:*
+ userId. Идентификатор сотрудника

### **AddUserAsync(UserAdd user)**
#### *Параметры:*
+ user Сотрудник

### **EditUserAsync(UserEdit user)**
Изменить сотрудника
#### *Параметры:*
+ user Сотрудник

### **AddAliasToUserAsync(ulong userId, string alias)**
Добавить сотруднику алиас почтового ящика
#### *Параметры:*
+ userId. Идентификатор сотрудника
+ alias. Алиас

### **DeleteAliasFromUserAsync(ulong userId, string alias)**
Удалить у сотрудника алиас почтового ящика.
#### *Параметры:*
+ userId. Идентификатор сотрудника
+ alias. Алиас

### **GetStatus2FAUserAsync(ulong userId)**
Возвращает информацию о статусе 2FA сотрудника.
#### *Параметры:*
+ userId. Идентификатор сотрудника

### **AddAliasToDepartmentAsync(ulong departmentId, string alias)**
Добавить подразделению алиас.
#### *Параметры:*
+ departmentId. Идентификатор подразделения.
+ alias. Алиас почтовой рассылки подразделения.

### **DeleteAliasFromDepartmentAsync(ulong departmentId, string alias)**
Удалить алиас почтовой рассылки подразделения.
#### *Параметры:*
+ departmentId. Идентификатор подразделения.
+ alias. Алиас.

### **AddDepartmentAsync(BaseDepartment department)**
Создать подразделение
#### *Параметры:*
+ department. Новое подразделение

### **GetDapartmentByIdAsync(long departmentId)**
Получить подразделение по ID
#### *Параметры:*
+ departmentId. Идентификатор подразделения.

### **GetDepartmentsAsync(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id)**
Получить список подразделений постранично
#### *Параметры:*
+ page. Номер страницы ответа.
+ perPage. Количество сотрудников на одной странице ответа.
+ parentId. Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.
+ orderBy. Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.

### **GetAllDepartmentsAsync(long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id)**
Получить полный список подразделений
#### *Параметры:*
+ parentId. Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.
+ orderBy. Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.

### **EditDepartmentAsync(Department department)**
Изменить подразделение
#### *Параметры:*
+ department. Подразделение.

### **DeleteDepartmentAsync(ulong departmentId)**
Удалить подразделение
+ departmentId. Идентификатор подразделения.

### **GetGroupsAsync(long page = 1, long perPage = 10)**
Получить список групп постранично
#### *Параметры:*
+ page. Номер страницы ответа.
+ perPage. Количество групп на одной странице ответа.

### **GetAllGroupsAsync()**
Получить полный списко групп

### **AddGroupAsync(BaseGroup group)**
Создать группу
#### *Параметры:*
+ group. Группа

### **DeleteGroupAsync(ulong groupId)**
Удалить группу
#### *Параметры:*
+ groupId. Идентификатор группы.

### **AddMemberToGroupAsync(long groupId, Member member)**
Добавить участника в группу
#### *Параметры:*
+ groupId. Идентификатор группы.
+ member. Участник группы.

### **DeleteMemderFromGroupAsync(ulong groupId, Member member)**
#### *Параметры:*
+ groupId. Идентификатор группы.
+ member. Участник группы.

### **GetGroupMembersAsync(ulong groupId)**
Получить список участников группы
#### *Параметры:*
+ groupId. Идентификатор группы.

### **GetGroupAsync(long groupId)**
Получить группу
#### *Параметры:*
+ groupId. Идентификатор группы.

### **EditGroupAsync(Group group)**
Изменить группу
#### *Параметры:*
group. Группа.

### **GetAllowListAsync()**
Получить список разрешенных IP-адресов и CIDR-подсетей.

### **SetAllowListAsync(List<string> allowlist)**
Создать/изменить список разрешенных IP-адресов и CIDR-подсетей.
#### *Параметры:*
+ allowlist. Список разрешенных IP-адресов и CIDR-подсетей.

### **DeleteAllowListAsync()**
Удалить список разрешенных IP-адресов и CIDR-подсетей.

### **GetStatus2faAsync()**
Получить статус обязательной двухфакторной аутентификации (2FA) для пользователей домена.

### **Enable2faAsync(EnableDomainStatus2FA status2FA)**
Включить обязательную двухфакторную аутентификацию (2FA) для пользователей домена.

### **Disable2faAsync()**
Выключить обязательную двухфакторную аутентификацию (2FA) для пользователей домена