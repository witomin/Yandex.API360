using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// DNS-запись
    /// </summary>
    public class DNSRecord {
        /// <summary>
        /// Адрес для записи А или ААА.
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// EXCHANGE для MX-записи.
        /// </summary>
        public string exchange { get; set; }
        /// <summary>
        /// Флаг для CAA-записи.
        /// </summary>
        public long flag { get; set; }
        /// <summary>
        /// Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Порт для SRV-записи.
        /// </summary>
        public long port { get; set; }
        /// <summary>
        /// PREFERENCE для MX-записи.
        /// </summary>
        public long preference { get; set; }
        /// <summary>
        /// Приоритет для SRV-записи.
        /// </summary>
        public long priority { get; set; }
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public long? recordId { get; set; }
        /// <summary>
        /// Тег для CAA-записи.
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// Цель для записи CNAME или SRV.
        /// </summary>
        public string target { get; set; }
        /// <summary>
        /// Содержимое для TXT-записи.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Время жизни записи в секундах (TTL)
        /// </summary>
        public long ttl { get; set; }
        /// <summary>
        /// Тип DNS записи
        /// </summary>
        public DNSRecordTypes type { get; set; }
        /// <summary>
        /// CAA-запись, заключенная в двойные кавычки, например "ca.example.net".
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// Вес для SRV-записи
        /// </summary>
        public long weight { get; set; }
    }
}
