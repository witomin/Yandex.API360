using System.Threading.Tasks;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для работы с настройками аутентификации
    /// </summary>
    public interface IAuthSettingsClient {
        /// <summary>
        /// Получить время жизни cookie сессии в секуднах
        /// </summary>
        /// <returns></returns>
        public Task<ulong> GetAuthTtlAsync();
        /// <summary>
        /// Установить время жизни cookie сессии
        /// </summary>
        /// <param name="authTTL">Время (в секундах), по истечении которого cookie сессии пользователей завершаются</param>
        /// <returns></returns>
        public Task<ulong> SetAuthTtlAsync(ulong authTTL);
        /// <summary>
        /// Выйти из аккаунта пользователя на всех устройствах
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public Task<ulong> LogoutUserAsync(ulong userId);
    }
}
