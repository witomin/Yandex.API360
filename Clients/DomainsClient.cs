using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DomainsClient : APIClient, IDomainsClient{
        public DomainsClient(Api360Options options) : base(options) { }

        public async Task<DomainList> GetDomainsAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DomainList>();
            return result;
        }

        public async Task<List<Domain>> GetAllDomainsAsync() {
            var result = new List<Domain>();
            var response = await GetDomainsAsync();
            //определяем сколько всего записей
            var TotalRecords = response.total;
            //пытаемся получить все записи в одном запросе
            response = await GetDomainsAsync(1, TotalRecords);
            //Проверяем все ли записи получены
            if (response.perPage == TotalRecords) {
                result = response.domains;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(response.domains);
                //определяем кол-во страниц ответа
                var pages = response.pages;
                //определяем сколько максимально отдает API
                var perPageMax = response.perPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    response = await GetDomainsAsync(i, perPageMax);
                    result.AddRange(response.domains);
                }
            }
            return result;
        }

        public async Task<Domain> AddDomainAsync(string domainName) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLdomains}", domainName);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<Domain>();
            return result;
        }

        public async Task DeleteDomainAsync(string domainName) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLdomains}/{domainName}");
            await CheckResponseAsync(response);
        }

        public async Task<DomainConnectStatus> GetDomainStatusAsync(string domainName) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}/{domainName}/status");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DomainConnectStatus>();
            return result;
        }
    }
}
