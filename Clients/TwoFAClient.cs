using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class TwoFAClient: APIClient, I2FAClient {
        public TwoFAClient(Api360Options options, ILogger<APIClient>? logger = default) :base(options, logger) { }
        public async Task<DomainStatus2FA> GetStatusAsync() {
            return await Get<DomainStatus2FA>($"{_options.URL2fa}");
        }

        public async Task<DomainStatus2FA> EnableAsync(EnableDomainStatus2FA status2FA) {
            if (status2FA is null) {
                throw new ArgumentNullException(nameof(status2FA));
            }
            return await Post<DomainStatus2FA>($"{_options.URL2fa}", status2FA);
        }

        public async Task<DomainStatus2FA> DisableAsync() {
            return await Delete<DomainStatus2FA>($"{_options.URL2fa}");
        }
    }
}
