using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class AuthSettingsClient : APIClient, IAuthSettingsClient {
        public AuthSettingsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }
        public async Task<ulong> GetAuthTtlAsync(CancellationToken cancellationToken = default) {
            var cooukiesSettings = await Get<CookiesSettings>($"{_options.URLsecurity}/domain_sessions", cancellationToken).ConfigureAwait(false);
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> SetAuthTtlAsync(ulong authTTL, CancellationToken cancellationToken = default) {
            var cooukiesSettings = await Post<CookiesSettings>($"{_options.URLsecurity}/domain_sessions", new CookiesSettings { authTTL = authTTL }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return cooukiesSettings.authTTL;
        }

        public async Task<ulong> LogoutUserAsync(ulong userId, CancellationToken cancellationToken = default) {
            var cooukiesSettings = await Put<CookiesSettings>($"{_options.URLsecurity}/domain_sessions/users/{userId}/logout", null, cancellationToken: cancellationToken).ConfigureAwait(false);
            return cooukiesSettings.authTTL;
        }
    }
}
