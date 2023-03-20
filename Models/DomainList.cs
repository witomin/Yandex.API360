using System.Collections.Generic;

namespace Yandex.API360.Models {
    public class DomainList {
        /// <summary>
        /// Список доменов
        /// </summary>
        public List<Domain> domains { get; set; }
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
