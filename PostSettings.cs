using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить почтовый адрес, с которого отправляются письма по умолчанию, и настройки подписей сотрудника
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Signatures> GetUserSignaturesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Signatures>();
        }
        /// <summary>
        /// Установить почтовый адрес сотрудника, с которого отправляются письма по умолчанию, и настройки его подписей
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="signatures"></param>
        /// <returns></returns>
        public async Task<Signatures> SetUserSignaturesAsync(ulong userId, Signatures signatures) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", signatures);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Signatures>();
        }
    }
}
