using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.API360.Models {
    /// <summary>
    /// Группа
    /// </summary>
    public class Group {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Название группы
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Тип группы
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Описание группы.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Количество участников группы
        /// </summary>
        public long membersCount { get; set; }
        /// <summary>
        /// Имя почтовой рассылки группы. Например, для адреса new-group@ваш-домен.ru имя почтовой рассылки — это new-group.
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// Адрес почтовой рассылки группы.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Алиасы почтовых ящиков группы
        /// </summary>
        public List<string> aliases { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор группы.
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// ризнак удаленной группы: true — группа удалена; false — группа действующая.
        /// </summary>
        public bool removed { get; set; }
        /// <summary>
        /// частники группы
        /// </summary>
        public List<Member> members { get; set; }
        /// <summary>
        /// Идентификаторы руководетелей группы.
        /// </summary>
        public List<ulong> adminIds { get; set; }
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
