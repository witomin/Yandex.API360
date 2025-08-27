using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для управления DNS
    /// </summary>
    public  interface IDNSClient {
        /// <summary>
        /// Получить DNS записи домена постранично
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа. Значение по умолчанию — 50.</param>
        /// <returns></returns>
        public Task<DNSList> GetListAsync(string domainName, long page = 1, long perPage = 10, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить все DNS записи домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <returns></returns>
        public Task<List<DNSRecord>> GetAllDNSAsync(string domainName, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="recordId">Идентификатор записи</param>
        /// <returns></returns>
        public Task DeleteAsync(string domainName, ulong recordId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавить DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode</param>
        /// <param name="dnsRecord">DNS запись</param>
        /// <returns></returns>
        public Task<DNSRecord> AddAsync(string domainName, DNSRecord dnsRecord, CancellationToken cancellationToken = default);
        /// <summary>
        /// Редактировать DNS-запись для домена
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="dnsRecord">Идентификатор записи</param>
        /// <returns></returns>
        public Task<DNSRecord> EditAsync(string domainName, DNSRecord dnsRecord, CancellationToken cancellationToken = default);
    }
}
