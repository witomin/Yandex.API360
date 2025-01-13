using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class RoutingClient: APIClient, IRoutingClient {
        public RoutingClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<List<Rule>> GetRulesAsync() {
            var result = await Get<RulesList>(_options.URLrouting);
            return result.rules;
        }

        public async Task<List<Rule>> SetRulesAsync(RulesList rulelist) {
            var result = await Put<RulesList>(_options.URLrouting, rulelist);
            return result.rules;
        }
    }
}
