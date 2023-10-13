using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Группа
    /// </summary>
    public class BaseGroup {
        /// <summary>
        /// Название группы
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Описание группы.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Имя почтовой рассылки группы. Например, для адреса new-group@ваш-домен.ru имя почтовой рассылки — это new-group.
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор группы.
        /// </summary>
        public string externalId { get; set; }
        /// <summary>
        /// Участники группы
        /// </summary>
        public List<Member> members { get; set; }
        /// <summary>
        /// Идентификаторы руководетелей группы.
        /// </summary>
        public List<ulong> adminIds { get; set; } 
    }
}
