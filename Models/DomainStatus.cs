using System;

namespace Yandex.API360.Models {
    /// <summary>
    /// Статус домена.
    /// </summary>
    public class DomainStatus {
        /// <summary>
        /// Значение статуса проверки DKIM
        /// </summary>
        public DomainStatusValue dkim { get; set; }
        /// <summary>
        /// Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Значение статуса проверки SPF
        /// </summary>
        public DomainStatusValue spf { get; set; }
        /// <summary>
        /// Значение статуса проверки MX
        /// </summary>
        public DomainStatusValue mx { get; set; }
        /// <summary>
        /// Значение статуса проверки NS
        /// </summary>
        public DomainStatusValue ns { get; set; }
        /// <summary>
        /// Дата и время последней проверки.
        /// </summary>
        public DateTime lastCheck { get; set; }
        /// <summary>
        /// Дата и время добавления домена.
        /// </summary>
        public DateTime lastAdded { get; set; }
    }
}
