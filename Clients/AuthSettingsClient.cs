using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AuthSettingsClient : APIClient, IAuthSettingsClient {
        public AuthSettingsClient(Api360Options options) : base(options) { }
        public async Task<ulong> GetAuthTtlAsync() {
            var response = await httpClient.GetAsync($"{_options.URLsecurity}/domain_sessions");
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> SetAuthTtlAsync(ulong authTTL) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLsecurity}/domain_sessions", new CookiesSettings { authTTL = authTTL });
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> LogoutUserAsync(ulong userId) {
            var response = await httpClient.PutAsync($"{_options.URLsecurity}/domain_sessions/users/{userId}/logout", null);
            await CheckResponseAsync(response);
            var cooukiesSettings = await response.Content.ReadFromJsonAsync<CookiesSettings>();
            return cooukiesSettings.authTTL;
        }
    }
}
