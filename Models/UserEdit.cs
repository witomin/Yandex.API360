using System.Collections.Generic;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Информация о сотруднике для метода редактирования
    /// </summary>
    public class UserEdit {
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public ulong id { get; set; }
        /// <summary>
        /// Описание сотрудника
        /// </summary>
        public string about { get; set; }
        /// <summary>
        /// Дата рождения сотрудника.
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// Идентификатор подразделения, в котором состоит сотрудник
        /// </summary>
        public ulong departmentId { get; set; }
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
        /// Список контактов сотрудника
        /// </summary>
        public List<BaseContact> contacts { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор сотрудника
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// Признак администратора организации: true — администратор; false — рядовой пользователь
        /// </summary>
        public bool isAdmin { get; set; }
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
        /// Пароль сотрудника
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Обязательность изменения пароля при первом входе: true — обязательно; false — необязательно.
        /// </summary>
        //public bool passwordChangeRequired { get; set; } // вызывает ошибку BadReguest
    }
}
