using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Yandex.API360{
    /// <summary>
    /// Endpoints для работы с настройкой антиспама
    /// </summary>
    public interface IAntispamClient {
        /// <summary>
        /// Получить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public Task<List<string>> GetAllowListAsync(CancellationToken cancellationToken = default) ;

        /// <summary>
        /// Создать/изменить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <param name="allowlist">Список разрешенных IP-адресов и CIDR-подсетей.</param>
        /// <returns></returns>
        public Task<object> SetAllowListAsync(List<string> allowlist, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public Task<object> DeleteAllowListAsync(CancellationToken cancellationToken = default);
    }
}
