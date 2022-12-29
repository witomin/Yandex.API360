using System;
using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Подразделение
    /// </summary>
    public class Department : BaseDepartment {
        /// <summary>
        /// Алиасы почтовых рассылок
        /// </summary>
        public List<string> aliases { get; set; }
        /// <summary>
        /// Дата и время создания подразделения
        /// </summary>
        public DateTime createdAt { get; set; }
        /// <summary>
        /// <summary>
        /// Адрес почтовой рассылки подразделения
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public ulong id { get; set; }
        /// <summary>
        /// Количество сотрудников подразделения с учетом вложенных подразделений.
        /// </summary>
        public long membersCount { get; set; }
    }
}
