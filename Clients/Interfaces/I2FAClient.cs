using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для работы с настройкой 2FA
    /// </summary>
    public interface I2FAClient {
        /// <summary>
        /// Получить статус обязательной двухфакторной аутентификации (2FA) для пользователей домена.
        /// </summary>
        /// <returns></returns>
        public Task<DomainStatus2FA> GetStatus2faAsync();
        /// <summary>
        /// Включить обязательную двухфакторную аутентификацию (2FA) для пользователей домена.
        /// </summary>
        /// <param name="status2FA"></param>
        /// <returns></returns>
        public Task<DomainStatus2FA> Enable2faAsync(EnableDomainStatus2FA status2FA);
        /// <summary>
        /// Выключить обязательную двухфакторную аутентификацию (2FA) для пользователей домена
        /// </summary>
        /// <returns></returns>
        public Task<DomainStatus2FA> Disable2faAsync();
    }
}
