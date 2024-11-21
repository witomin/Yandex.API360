using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Получить полный список доменов
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Добавить домен
        /// </summary>
        /// <param name="domainName">Полное имя домена</param>
        /// <returns></returns>
        public async Task<Domain> AddDomainAsync(string domainName) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLdomains}", domainName);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<Domain>();
            return result;
        }
        /// <summary>
        /// Удалить домен. Вы можете удалить любой домен, кроме технического.
        /// </summary>
        /// <param name="domainName">Полное имя домена</param>
        /// <returns></returns>
        public async Task DeleteDomainAsync(string domainName) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLdomains}/{domainName}");
            await CheckResponseAsync(response);
        }
        /// <summary>
        /// Получить статус подключения домена
        /// </summary>
        /// <param name="domainName">Полное имя домена. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <returns></returns>
        public async Task<DomainConnectStatus> GetDomainStatusAsync(string domainName) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}/{domainName}/status");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DomainConnectStatus>();
            return result;
        }
    }
}
