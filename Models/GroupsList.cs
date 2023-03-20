using System.Collections.Generic;
/// <summary>
/// Список групп
/// </summary>
namespace Yandex.API360.Models {
    public class GroupsList {
        /// <summary>
        /// Список групп
        /// </summary>
        public List<Group> groups { get; set; }
        /// <summary>
        /// Номер страницы ответа
        /// </summary>
        public long page { get; set; }
        /// <summary>
        /// Количество страниц ответа
        /// </summary>
        public long pages { get; set; }
        /// <summary>
        /// Количество групп на одной странице ответа
        /// </summary>
        public long perPage { get; set; }
        /// <summary>
        /// Общее количество групп
        /// </summary>
        public long total { get; set; }
    }
}
