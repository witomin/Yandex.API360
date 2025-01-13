using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AntispamClient: APIClient, IAntispamClient {
        public AntispamClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<List<string>> GetAllowListAsync() {
            var result = await Get<WhiteList>($"{_options.URLAntispam}");
            return result.allowList;
        }

        public async Task<object> SetAllowListAsync(List<string> allowlist) {
            return await Post<object>($"{_options.URLAntispam}", new WhiteList { allowList = allowlist });
        }

        public async Task<object> DeleteAllowListAsync() {
            return await Delete<object>($"{_options.URLAntispam}");
        }
    }
}
