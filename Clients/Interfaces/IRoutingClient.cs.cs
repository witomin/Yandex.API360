using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints управления правилами обработки писем
    /// </summary>
    public  interface IRoutingClient {
        /// <summary>
        /// Получить правила обработки писем
        /// </summary>
        /// <returns></returns>
        public Task<List<Rule>> GetRulesAsync();
        /// <summary>
        /// Задать правила обработки писем
        /// </summary>
        /// <param name="rulelist">Список правил</param>
        /// <returns></returns>
        public  Task<List<Rule>> SetRulesAsync(RulesList rulelist);
        }
}
