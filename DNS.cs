using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить DNS записи домена постранично
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа. Значение по умолчанию — 50.</param>
        /// <returns></returns>
        public async Task<DNSList> GetDNSAsync(string domainName, long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}/{domainName}/dns?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSList>();
            return result;
        }
        public async Task<DNSList> GetAllDNSAsync() {
            throw new NotImplementedException();
        }
    }
}
