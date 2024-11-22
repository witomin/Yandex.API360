using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Clients.Interfaces;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class RoutingClient: APIClient, IRoutingClient {
        public RoutingClient(Api360Options options) : base(options) { }

        public async Task<List<Rule>> GetRulesAsync() {
            var response = await httpClient.GetAsync(_options.URLrouting);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }

        public async Task<List<Rule>> SetRulesAsync(RulesList rulelist) {
            var response = await httpClient.PutAsJsonAsync(_options.URLrouting, rulelist);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }
    }
}
