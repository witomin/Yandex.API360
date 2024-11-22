using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для управления организациями
    /// </summary>
    public interface IOrganizationClient {
        /// <summary>
        /// Получить организации постранично
        /// </summary>
        /// <param name="pageSize">Количество организаций на странице. Максимальное значение — 100. По умолчанию — 10.</param>
        /// <param name="pageToken">Токен постраничной навигации.</param>
        /// <returns></returns>
        public Task<OrganizationList> GetOrganizationsAsync(int? pageSize = 10, string? pageToken = null);
        /// <summary>
        /// Получить полный список организаций
        /// </summary>
        /// <returns></returns>
        public Task<List<Organization>> GetAllOrganizationsAsync();
    }
}
