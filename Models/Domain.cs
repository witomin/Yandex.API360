namespace Yandex.API360.Models {
    /// <summary>
    /// Домен
    /// </summary>
    public class Domain {
        /// <summary>
        /// Полное доменное имя.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Страна домена.
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Указывает ли MX-запись на сервер mx.yandex.net. true — указывает; false — не указывает.
        /// </summary>
        public bool mx { get; set; }
        /// <summary>
        /// Признак домена, делегированного на серверы Яндекса. true — делегирован; false — не делегирован.
        /// </summary>
        public bool delegated { get; set; }
        /// <summary>
        /// Признак основного домена. true — основной; false — домен-алиас (дополнительный).
        /// </summary>
        public bool master { get; set; }
        /// <summary>
        /// Признак подтвержденного домена. true — подтвержден; false — не подтвержден.
        /// </summary>
        public bool verified { get; set; }
        /// <summary>
        /// Статус домена.
        /// </summary>
        public DomainStatus status { get; set; }
    }
}

