using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить список доменов постранично постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество доменов на одной странице ответа. Значение по умолчанию — 10.</param>
        /// <returns></returns>
        public async Task<DomainList> GetDomainsAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DomainList>();
            return result;
        }
    }
}
