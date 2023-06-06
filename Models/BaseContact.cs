using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    public class BaseContact {
        /// <summary>
        /// Тип контакта
        /// </summary>
        public ContactTypes? type { get; set; }
        /// <summary>
        /// Значение контакта
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// Произвольная метка контакта
        /// </summary>
        public string label { get; set; }
    }
}
