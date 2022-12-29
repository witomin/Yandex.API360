using System;
using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Группа
    /// </summary>
    public class Group: BaseGroup {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public ulong id { get; set; }
        /// <summary>
        /// Тип группы
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Количество участников группы
        /// </summary>
        public long membersCount { get; set; }
        /// <summary>
        /// Адрес почтовой рассылки группы.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Алиасы почтовых ящиков группы
        /// </summary>
        public List<string> aliases { get; set; }
        /// <summary>
        /// ризнак удаленной группы: true — группа удалена; false — группа действующая.
        /// </summary>
        public bool removed { get; set; }
        /// <summary>
        /// Идентификатор создателя группы
        /// </summary>
        public ulong authorId { get; set; }
        /// <summary>
        /// Идентификаторы групп, в которые входит эта группа.
        /// </summary>
        public List<long> memberOf { get; set; }
        /// <summary>
        /// Дата и время создания группы.
        /// </summary>
        public DateTime createdAt { get; set; }
    }
}
