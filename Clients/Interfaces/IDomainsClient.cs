using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints управления доменами
    /// </summary>
    public interface IDomainsClient {
        /// <summary>
        /// Получить список доменов постранично постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество доменов на одной странице ответа. Значение по умолчанию — 10.</param>
        /// <returns></returns>
        public Task<DomainList> GetDomainsAsync(long page = 1, long perPage = 10);
        /// <summary>
        /// Получить полный список доменов
        /// </summary>
        /// <returns></returns>
        public Task<List<Domain>> GetAllDomainsAsync();
        /// <summary>
        /// Добавить домен
        /// </summary>
        /// <param name="domainName">Полное имя домена</param>
        /// <returns></returns>
        public Task<Domain> AddDomainAsync(string domainName);
        /// <summary>
        /// Удалить домен. Вы можете удалить любой домен, кроме технического.
        /// </summary>
        /// <param name="domainName">Полное имя домена</param>
        /// <returns></returns>
        public Task DeleteDomainAsync(string domainName);
        /// <summary>
        /// Получить статус подключения домена
        /// </summary>
        /// <param name="domainName">Полное имя домена. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <returns></returns>
        public Task<DomainConnectStatus> GetDomainStatusAsync(string domainName);
    }
}
