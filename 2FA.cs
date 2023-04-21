using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить статус обязательной двухфакторной аутентификации (2FA) для пользователей домена.
        /// </summary>
        /// <returns></returns>
        public async Task<DomainStatus2FA> GetStatus2faAsync() {
            var response = await httpClient.GetAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
        /// <summary>
        /// Включить обязательную двухфакторную аутентификацию (2FA) для пользователей домена.
        /// </summary>
        /// <param name="status2FA"></param>
        /// <returns></returns>
        public async Task<DomainStatus2FA> Enable2faAsync(EnableDomainStatus2FA status2FA) {
            if (status2FA is null) {
                throw new ArgumentNullException(nameof(status2FA));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URL2fa}", status2FA);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
        /// <summary>
        /// Выключить обязательную двухфакторную аутентификацию (2FA) для пользователей домена
        /// </summary>
        /// <returns></returns>
        public async Task<DomainStatus2FA> Disable2faAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
    }
}
