using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class RoutingClient: APIClient, IRoutingClient {
        public RoutingClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<List<Rule>> GetRulesAsync(CancellationToken cancellationToken = default) {
            var result = await Get<RulesList>(_options.URLrouting, cancellationToken).ConfigureAwait(false);
            return result.rules;
        }

        public async Task<List<Rule>> SetRulesAsync(RulesList rulelist, CancellationToken cancellationToken = default) {
            var result = await Put<RulesList>(_options.URLrouting, rulelist, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.rules;
        }
    }
}
