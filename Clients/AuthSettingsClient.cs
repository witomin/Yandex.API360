using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AuthSettingsClient : APIClient, IAuthSettingsClient {
        public AuthSettingsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }
        public async Task<ulong> GetAuthTtlAsync() {
            var cooukiesSettings = await Get<CookiesSettings>($"{_options.URLsecurity}/domain_sessions");
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> SetAuthTtlAsync(ulong authTTL) {
            var cooukiesSettings = await Post<CookiesSettings>($"{_options.URLsecurity}/domain_sessions", new CookiesSettings { authTTL = authTTL });
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> LogoutUserAsync(ulong userId) {
            var cooukiesSettings = await Put<CookiesSettings>($"{_options.URLsecurity}/domain_sessions/users/{userId}/logout", null);
            return cooukiesSettings.authTTL;
        }
    }
}
