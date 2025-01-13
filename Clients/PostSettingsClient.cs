using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class PostSettingsClient: APIClient, IPostSettingsClient {
        public PostSettingsClient (Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<UserPersonalSettings> GetAsync(ulong userId) {
            return await Get<UserPersonalSettings>($"{_options.URLPostSettings}/users/{userId}/settings/sender_info");
        }

        public async Task<UserPersonalSettings> SetAsync(ulong userId, UserPersonalSettings UserSettings) {
            if (UserSettings is null) {
                throw new ArgumentNullException(nameof(UserSettings));
            }
            return await Post<UserPersonalSettings>($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", UserSettings);
        }

        public async Task<CollectAddressStatus> GetСollectAddressesAsync(ulong userId) {
            return await Get<CollectAddressStatus>($"{_options.URLPostSettings}/users/{userId}/settings/address_book");
        }

        public async Task<CollectAddressStatus> SetСollectAddressesAsync(ulong userId, CollectAddressStatus collectAddresses) {
            if (collectAddresses is null) {
                throw new ArgumentNullException(nameof(collectAddresses));
            }
            return await Post<CollectAddressStatus>($"{_options.URLPostSettings}/users/{userId}/settings/address_book", collectAddresses);
        }

        public async Task<UserRulesList> GetUserRulesAsync(ulong userId) {
            return await Get<UserRulesList>($"{_options.URLPostSettings}/users/{userId}/settings/user_rules");
        }

        public async Task<ulong> AddUserRuleAsync(ulong userId, UserRule userRule) {
            if (userRule is null) {
                throw new ArgumentNullException(nameof(userRule));
            }
            var result = await Post<UserRuleAddResponse>($"{_options.URLPostSettings}/users/{userId}/settings/user_rules", userRule);
            return result.ruleId;
        }

        public async Task DeleteUserRuleAsync(ulong userId, ulong ruleId) {
            await Delete($"{_options.URLPostSettings}/users/{userId}/settings/user_rules/{ruleId}");
        }
    }
}
