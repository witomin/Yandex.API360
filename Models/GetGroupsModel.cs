using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.API360.Models {
    class GetGroupsModel {
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
        /// Количество сотрудников на одной странице ответа
        /// </summary>
        public long perPage { get; set; }
        /// <summary>
        /// Общее количество сотрудников
        /// </summary>
        public long total { get; set; }
    }
}
