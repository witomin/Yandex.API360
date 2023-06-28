using System.Collections.Generic;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    public class UserPersonalSettings {
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string fromName { get; set; }
        /// <summary>
        /// Отправлять письма с адреса
        /// </summary>
        public string defaultFrom { get; set; }
        /// <summary>
        /// Подписи
        /// </summary>
        public List<Sign> signs { get; set; }
        /// <summary>
        /// Расположение подписи
        /// </summary>
        public SignPositions signPosition { get; set; }
    }

}
