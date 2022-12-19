using System;
using System.Collections.Generic;

namespace Yandex.API360.Models {
    public class User {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public ulong id { get; set; }
        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// Идентификатор подразделения, в котором состоит сотрудник
        /// </summary>
        public long departmentId { get; set; }
        /// <summary>
        /// Основной адрес электронной почты сотрудника
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public Name name { get; set; }
        /// <summary>
        /// Пол сотрудника
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// Идентификатор портрета сотрудника.
        /// </summary>
        public string avatarId { get; set; }
        /// <summary>
        /// Описание сотрудника
        /// </summary>
        public string about { get; set; }
        /// <summary>
        /// Дата рождения сотрудника.
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// Список контактов сотрудника
        /// </summary>
        public List<Contact> contacts { get; set; }
        /// <summary>
        /// Список алиасов сотрудника.
        /// </summary>
        public List<string> aliases { get; set; }
        /// <summary>
        /// Список идентифиакторов групп, в которых состоит сотрудник
        /// </summary>
        public List<int>? groups { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор сотрудника
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// Признак администратора организации: true — администратор; false — рядовой пользователь
        /// </summary>
        public bool isAdmin { get; set; }
        /// <summary>
        /// Признак служебных сотрудников-ботов: true — бот; false — человек.
        /// </summary>
        public bool isRobot { get; set; }
        /// <summary>
        /// Статус сотрудника: true — уволенный; false — действующий.
        /// </summary>
        public bool isDismissed { get; set; }
        /// <summary>
        /// Статус аккаунта сотрудника: true — активен; false — заблокирован
        /// </summary>
        public bool isEnabled { get; set; }
        /// <summary>
        /// Часовой пояс сотрудника
        /// </summary>
        public string timezone { get; set; }
        /// <summary>
        /// Язык сотрудника
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// Дата и время создания сотрудника
        /// </summary>
        public DateTime createdAt { get; set; }
        /// <summary>
        /// Дата и время изменения сотрудника
        /// </summary>
        public DateTime updatedAt { get; set; }
    }
}
