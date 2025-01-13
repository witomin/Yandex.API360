using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DNSClient :APIClient, IDNSClient {
        public DNSClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { } 

        public async Task<DNSList> GetListAsync(string domainName, long page = 1, long perPage = 10) {
            return await Get<DNSList>($"{_options.URLdomains}/{domainName}/dns?page={page}&perPage={perPage}");
        }

        public async Task<List<DNSRecord>> GetAllDNSAsync(string domainName) {
            var result = new List<DNSRecord>();
            var response = await GetListAsync(domainName);
            //определяем сколько всего записей
            var TotalRecords = response.total;
            //пытаемся получить все записи в одном запросе
            response = await GetListAsync(domainName, 1, TotalRecords);
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
                    response = await GetListAsync(domainName, i, perPageMax);
                    result.AddRange(response.records);
                }
            }
            return result;
        }

        public async Task DeleteAsync(string domainName, ulong recordId) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            await Delete($"{_options.URLdomains}/{domainName}/dns/{recordId}");
        }

        public async Task<DNSRecord> AddAsync(string domainName, DNSRecord dnsRecord) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            if (dnsRecord is null) {
                throw new ArgumentNullException(nameof(dnsRecord));
            }
            return await Post<DNSRecord>($"{_options.URLdomains}/{domainName}/dns", dnsRecord);
        }

        public async Task<DNSRecord> EditAsync(string domainName, DNSRecord dnsRecord) {
            if (dnsRecord is null) {
                throw new ArgumentNullException(nameof(dnsRecord));
            }
            if (dnsRecord.recordId is null) {
                throw new ArgumentNullException(nameof(dnsRecord.recordId));
            }
            return await Post<DNSRecord>($"{_options.URLdomains}/{domainName}/dns/{dnsRecord.recordId}", dnsRecord);
        }
    }
}
