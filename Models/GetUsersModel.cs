using System.Collections.Generic;

namespace Yandex.API360.Models {
    class GetUsersModel {
        public List<User> users { get; set; }
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
