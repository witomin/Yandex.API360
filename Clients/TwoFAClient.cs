using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class TwoFAClient: APIClient, I2FAClient {
        public TwoFAClient(Api360Options options, ILogger<APIClient>? logger = default) :base(options, logger) { }
        public async Task<DomainStatus2FA> GetStatusAsync(CancellationToken cancellationToken = default) {
            return await Get<DomainStatus2FA>($"{_options.URL2fa}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<DomainStatus2FA> EnableAsync(EnableDomainStatus2FA status2FA, CancellationToken cancellationToken = default) {
            if (status2FA is null) {
                throw new ArgumentNullException(nameof(status2FA));
            }
            return await Post<DomainStatus2FA>($"{_options.URL2fa}", status2FA, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<DomainStatus2FA> DisableAsync(CancellationToken cancellationToken = default) {
            return await Delete<DomainStatus2FA>($"{_options.URL2fa}", cancellationToken).ConfigureAwait(false);
        }
    }
}
