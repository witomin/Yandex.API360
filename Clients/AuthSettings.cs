using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить время жизни cookie сессии в секуднах
        /// </summary>
        /// <returns></returns>
        public async Task<ulong> GetAuthTtlAsync() {
            var response = await httpClient.GetAsync($"{_options.URLsecurity}/domain_sessions");
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }
        /// <summary>
        /// Установить время жизни cookie сессии
        /// </summary>
        /// <param name="authTTL">Время (в секундах), по истечении которого cookie сессии пользователей завершаются</param>
        /// <returns></returns>
        public async Task<ulong> SetAuthTtlAsync(ulong authTTL) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLsecurity}/domain_sessions", new CookiesSettings { authTTL = authTTL });
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }
        /// <summary>
        /// Выйти из аккаунта пользователя на всех устройствах
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<ulong> LogoutUserAsync(ulong userId) {
            var response = await httpClient.PutAsync($"{_options.URLsecurity}/domain_sessions/users/{userId}/logout", null);
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }
    }
}
