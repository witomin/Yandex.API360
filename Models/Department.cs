using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Подразделение
    /// </summary>
    public class Department {
        /// <summary>
        /// Алиасы почтовых рассылок
        /// </summary>
        public List<string> aliases { get; set; }
        /// <summary>
        /// Дата и время создания подразделения
        /// </summary>
        public string createdAt { get; set; }
        /// <summary>
        /// Описание подразделения
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Адрес почтовой рассылки подразделения
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор подразделения
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// Руководитель подразделения
        /// </summary>
        public string headId { get; set; }
        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Имя почтовой рассылки подразделения. Например, для адреса new-department@ваш-домен.ru имя почтовой рассылки — это new-department.
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// Количество сотрудников подразделения с учетом вложенных подразделений.
        /// </summary>
        public long membersCount { get; set; }
        /// <summary>
        /// Название подразделения
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public long parentId { get; set; }
    }
}
