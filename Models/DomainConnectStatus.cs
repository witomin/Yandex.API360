using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Статус подключения домена
    /// </summary>
    public class DomainConnectStatus {
        /// <summary>
        /// Методы подтверждения
        /// </summary>
        public List<ConfirmationMethod> methods { get; set; }
        /// <summary>
        /// Статус домена
        /// </summary>
        public string status { get; set; }
    }
}
