using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class TwoFAClient: APIClient, I2FAClient {
        public TwoFAClient(Api360Options options):base(options) { }
        public async Task<DomainStatus2FA> GetStatusAsync() {
            var response = await httpClient.GetAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }

        public async Task<DomainStatus2FA> EnableAsync(EnableDomainStatus2FA status2FA) {
            if (status2FA is null) {
                throw new ArgumentNullException(nameof(status2FA));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URL2fa}", status2FA);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }

        public async Task<DomainStatus2FA> DisableAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
    }
}
