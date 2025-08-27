using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        [Obsolete("Используйте методы Client.Organizations"/*, true*/)]
        /// <summary>
        /// Получить организации постранично
        /// </summary>
        /// <param name="pageSize">Количество организаций на странице. Максимальное значение — 100. По умолчанию — 10.</param>
        /// <param name="pageToken">Токен постраничной навигации.</param>
        /// <returns></returns>
        public async Task<OrganizationList> GetOrganizationsAsync(int? pageSize = 10, string? pageToken = null) {
            var response = await _httpClient.GetAsync($"{_options.URLOrg}?pageSize={pageSize}{(pageToken != null ? $"&pageToken={pageToken}" : string.Empty)}");
            await CheckResponseAsync(response);
            var organisations = await response.Content.ReadFromJsonAsync<OrganizationList>();
            return organisations;
        }
        [Obsolete("Используйте методы Client.Organizations"/*, true*/)]
        /// <summary>
        /// Получить полный список организаций
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetAllOrganizationsAsync() {
            var result = new List<Organization>();
            var response = await GetOrganizationsAsync(_options.MaxCountOrgInResponse);
            if (string.IsNullOrEmpty(response.nextPageToken)) {
                result = response.organizations;
            }
            else {
                result.AddRange(response.organizations);
                do {
                    response = await GetOrganizationsAsync(_options.MaxCountOrgInResponse);
                    result.AddRange(response.organizations);
                }
                while (string.IsNullOrEmpty(response.nextPageToken));
            }
            return result;
        }
    }
}
