using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class PostSettingsClient: APIClient, IPostSettingsClient {
        public PostSettingsClient (Api360Options options) : base(options) { }

        public async Task<UserPersonalSettings> GetUserPersonalSettingsAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserPersonalSettings>();
        }

        public async Task<UserPersonalSettings> SetUserPersonalSettingsAsync(ulong userId, UserPersonalSettings UserSettings) {
            if (UserSettings is null) {
                throw new ArgumentNullException(nameof(UserSettings));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", UserSettings);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserPersonalSettings>();
        }

        public async Task<CollectAddressStatus> GetСollectAddressesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/address_book");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<CollectAddressStatus>();
        }

        public async Task<CollectAddressStatus> SetСollectAddressesAsync(ulong userId, CollectAddressStatus collectAddresses) {
            if (collectAddresses is null) {
                throw new ArgumentNullException(nameof(collectAddresses));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/address_book", collectAddresses);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<CollectAddressStatus>();
        }

        public async Task<UserRulesList> GetUserRulesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserRulesList>();
        }

        public async Task<ulong> AddUserRuleAsync(ulong userId, UserRule userRule) {
            if (userRule is null) {
                throw new ArgumentNullException(nameof(userRule));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules", userRule);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<UserRuleAddResponse>();
            return result.ruleId;
        }

        public async Task DeleteUserRuleAsync(ulong userId, ulong ruleId) {
            var response = await httpClient.DeleteAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules/{ruleId}");
            await CheckResponseAsync(response);
        }
    }
}
