using System.Collections.Generic;

namespace Yandex.API360.Models {
    class GetUsersAPIResponse {
        public List<User> users { get; set; }
        /// <summary>
        /// Номер страницы ответа
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// Количество страниц ответа
        /// </summary>
        public int pages { get; set; }
        /// <summary>
        /// Количество сотрудников на одной странице ответа
        /// </summary>
        public int perPage { get; set; }
        /// <summary>
        /// Общее количество сотрудников
        /// </summary>
        public int total { get; set; }
    }


    

   

    

}
