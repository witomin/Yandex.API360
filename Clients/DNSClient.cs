using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DNSClient :APIClient, IDNSClient {
        public DNSClient(Api360Options options) : base(options) { } 

        public async Task<DNSList> GetDNSAsync(string domainName, long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}/{domainName}/dns?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSList>();
            return result;
        }

        public async Task<List<DNSRecord>> GetAllDNSAsync(string domainName) {
            var result = new List<DNSRecord>();
            var response = await GetDNSAsync(domainName);
            //определяем сколько всего записей
            var TotalRecords = response.total;
            //пытаемся получить все записи в одном запросе
            response = await GetDNSAsync(domainName, 1, TotalRecords);
            //Проверяем все ли записи получены
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

        public async Task DeleteDNSAsync(string domainName, ulong recordId) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLdomains}/{domainName}/dns/{recordId}");
            await CheckResponseAsync(response);
        }

        public async Task<DNSRecord> AddDNSAsync(string domainName, DNSRecord dnsRecord) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            if (dnsRecord is null) {
                throw new ArgumentNullException(nameof(dnsRecord));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLdomains}/{domainName}/dns", dnsRecord);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSRecord>();
            return result;
        }

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
