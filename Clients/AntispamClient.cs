using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AntispamClient: APIClient, IAntispamClient {
        public AntispamClient(Api360Options options) : base(options) { }

        public async Task<List<string>> GetAllowListAsync() {
            var response = await httpClient.GetAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<WhiteList>();
            return result.allowList;
        }

        public async Task<object> SetAllowListAsync(List<string> allowlist) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLAntispam}", new WhiteList { allowList = allowlist });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }

        public async Task<object> DeleteAllowListAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
    }
}
