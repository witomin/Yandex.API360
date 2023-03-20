using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Списко организаций. Ответ API.
    /// </summary>
    public class OrganizationList {
        /// <summary>
        /// Список организаций
        /// </summary>
        public List<Organization> organizations { get; set; }
        /// <summary>
        /// Токен постраничной навигации
        /// </summary>
        public string nextPageToken { get; set; }
    }
}
