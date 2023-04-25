using System.Collections.Generic;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Подпись
    /// </summary>
    public class Sign {
        /// <summary>
        /// Привязать к адресу
        /// </summary>
        public List<string> emails { get; set; }
        /// <summary>
        /// Является ли подписью по умолчанию.
        /// </summary>
        public bool isDefault { get; set; }
        /// <summary>
        /// Текст подписи, поддерживает разметку html, в том числе изображения.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Язык подписи
        /// </summary>
        public Languages lang { get; set; }
    }

}
