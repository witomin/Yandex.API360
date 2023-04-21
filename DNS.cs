using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Получить все DNS записи домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <returns></returns>
        public async Task<List<DNSRecord>> GetAllDNSAsync(string domainName) {
            var result = new List<DNSRecord>();
            var response = await GetDNSAsync(domainName);
            //определяем сколько всего записей
            var TotalRecords = response.total;
            //пытаемся получить все подразделения в одном запросе
            response = await GetDNSAsync(domainName, 1, TotalRecords);
            //Проверяем все ли подразделения получены
            if (response.perPage == TotalRecords) {
                result = response.records;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(response.records);
                //определяем кол-во страниц ответа
                var pages = response.pages;
                //определяем сколько максимально отдает API
                var perPageMax = response.perPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    response = await GetDNSAsync(domainName, i, perPageMax);
                    result.AddRange(response.records);
                }
            }
            return result;
        }
        /// <summary>
        /// Удалить DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="recordId">Идентификатор записи</param>
        /// <returns></returns>
        public async Task DeleteDNSAsync(string domainName, ulong recordId) {
            var response = await httpClient.DeleteAsync($"{_options.URLdomains}/{domainName}/dns/{recordId}");
            await CheckResponseAsync(response);
        }
        /// <summary>
        /// Добавить DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode</param>
        /// <param name="dnsRecord">DNS запись</param>
        /// <returns></returns>
        public async Task<DNSRecord> AddDNSAsync(string domainName, DNSRecord dnsRecord) {
            if (dnsRecord is null) {
                throw new ArgumentNullException(nameof(dnsRecord));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLdomains}/{domainName}/dns", dnsRecord);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSRecord>();
            return result;
        }
        /// <summary>
        /// Редактировать DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="dnsRecord">Идентификатор записи</param>
        /// <returns></returns>
        public async Task<DNSRecord> EditDNSAsync(string domainName, DNSRecord dnsRecord) {
            if (dnsRecord is null) {
                throw new ArgumentNullException(nameof(dnsRecord));
            }
            if (dnsRecord.recordId is null) {
                throw new ArgumentNullException(nameof(dnsRecord.recordId));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLdomains}/{domainName}/dns/{dnsRecord.recordId}", dnsRecord);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSRecord>();
            return result;
        }
    }
}
