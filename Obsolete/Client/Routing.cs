using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        [Obsolete("Используйте методы Client.Routing"/*, true*/)]
        /// <summary>
        /// Получить правила обработки писем
        /// </summary>
        /// <returns></returns>
        public async Task<List<Rule>> GetRulesAsync() {
            var response = await httpClient.GetAsync(_options.URLrouting);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }
        [Obsolete("Используйте методы Client.Routing"/*, true*/)]
        /// <summary>
        /// Задать правила обработки писем
        /// </summary>
        /// <param name="rulelist">Список правил</param>
        /// <returns></returns>
        public async Task<List<Rule>> SetRulesAsync(RulesList rulelist) {
            var response = await httpClient.PutAsJsonAsync(_options.URLrouting, rulelist);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }
    }
}
