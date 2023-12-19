using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.API360.Models {
    /// <summary>
    /// Список событий в аудит логе Диска организации
    /// </summary>
    public class EventList {
        /// <summary>
        /// Список событий
        /// </summary>
        public List<Event> events { get; set; }
        /// <summary>
        /// Токен для получения следующей страницы постраничной навигации
        /// </summary>
        public string nextPageToken { get; set; }
    }
}
