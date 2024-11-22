using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        [Obsolete("Используйте методы Client.Antispam"/*, true*/)]
        /// <summary>
        /// Получить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllowListAsync() {
            var response = await httpClient.GetAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<WhiteList>();
            return result.allowList;
        }
        [Obsolete("Используйте методы Client.Antispam"/*, true*/)]
        /// <summary>
        /// Создать/изменить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <param name="allowlist">Список разрешенных IP-адресов и CIDR-подсетей.</param>
        /// <returns></returns>
        public async Task<object> SetAllowListAsync(List<string> allowlist) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLAntispam}", new WhiteList { allowList = allowlist });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
        [Obsolete("Используйте методы Client.Antispam"/*, true*/)]
        /// <summary>
        /// Удалить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<object> DeleteAllowListAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
    }
}
