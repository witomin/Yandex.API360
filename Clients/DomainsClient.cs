using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DomainsClient : APIClient, IDomainsClient{
        public DomainsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<DomainList> GetListAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default) {
            return await Get<DomainList>($"{_options.URLdomains}?page={page}&perPage={perPage}").ConfigureAwait(false);
        }

        public async Task<List<Domain>> GetAllDomainsAsync(CancellationToken cancellationToken = default) {
            var result = new List<Domain>();
            var response = await GetListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            //определяем сколько всего записей
            var TotalRecords = response.total;
            //пытаемся получить все записи в одном запросе
            response = await GetListAsync(1, TotalRecords, cancellationToken).ConfigureAwait(false);
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
                    response = await GetListAsync(i, perPageMax, cancellationToken).ConfigureAwait(false);
                    result.AddRange(response.domains);
                }
            }
            return result;
        }

        public async Task<Domain> AddAsync(string domainName, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            return await Post<Domain>($"{_options.URLdomains}", domainName, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(string domainName, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(domainName)) {
                throw new ArgumentNullException(nameof(domainName));
            }
            await Delete($"{_options.URLdomains}/{domainName}", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<DomainConnectStatus> GetStatusAsync(string domainName, CancellationToken cancellationToken = default) {
            return await Get<DomainConnectStatus>($"{_options.URLdomains}/{domainName}/status", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
