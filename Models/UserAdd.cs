using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    /// <summary>
    /// Информация о сотруднике для метода редактирования
    /// </summary>
    public class UserAdd {
        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// Описание сотрудника
        /// </summary>
        public string? about { get; set; }
        /// <summary>
        /// Дата рождения сотрудника.
        /// </summary>
        public string? birthday { get; set; }
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
        public string? gender { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string? position { get; set; }
        /// <summary>
        /// Список контактов сотрудника
        /// </summary>
        public List<BaseContact>? contacts { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор сотрудника
        /// </summary>
        public string? externalId { get; set; }
        /// <summary>
        /// Признак администратора организации: true — администратор; false — рядовой пользователь
        /// </summary>
        public bool isAdmin { get; set; }
        /// <summary>
        /// Часовой пояс сотрудника
        /// </summary>
        public string? timezone { get; set; }
        /// <summary>
        /// Язык сотрудника
        /// </summary>
        public string? language { get; set; }
        /// <summary>
        /// Пароль сотрудника
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Публичное имя сотрудника – имя, которое сотрудник использует в своем профиле для представления себя, оно может совпадать с реальным именем, быть псевдонимом или никнеймом.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

    }
}
