using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AntispamClient: APIClient, IAntispamClient {
        public AntispamClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<List<string>> GetAllowListAsync(CancellationToken cancellationToken = default) {
            var result = await Get<WhiteList>($"{_options.URLAntispam}", cancellationToken).ConfigureAwait(false);
            return result.allowList;
        }

        public async Task<object> SetAllowListAsync(List<string> allowlist, CancellationToken cancellationToken = default) {
            return await Post<object>($"{_options.URLAntispam}", new WhiteList { allowList = allowlist }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<object> DeleteAllowListAsync(CancellationToken cancellationToken = default) {
            return await Delete<object>($"{_options.URLAntispam}", cancellationToken);
        }
    }
}
