using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class PostSettingsClient: APIClient, IPostSettingsClient {
        public PostSettingsClient (Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<UserPersonalSettings> GetAsync(ulong userId, CancellationToken cancellationToken = default) {
            return await Get<UserPersonalSettings>($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserPersonalSettings> SetAsync(ulong userId, UserPersonalSettings UserSettings, CancellationToken cancellationToken = default) {
            if (UserSettings is null) {
                throw new ArgumentNullException(nameof(UserSettings));
            }
            return await Post<UserPersonalSettings>($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", UserSettings, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<CollectAddressStatus> GetСollectAddressesAsync(ulong userId, CancellationToken cancellationToken = default) {
            return await Get<CollectAddressStatus>($"{_options.URLPostSettings}/users/{userId}/settings/address_book", cancellationToken).ConfigureAwait(false);
        }

        public async Task<CollectAddressStatus> SetСollectAddressesAsync(ulong userId, CollectAddressStatus collectAddresses, CancellationToken cancellationToken = default) {
            if (collectAddresses is null) {
                throw new ArgumentNullException(nameof(collectAddresses));
            }
            return await Post<CollectAddressStatus>($"{_options.URLPostSettings}/users/{userId}/settings/address_book", collectAddresses, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<UserRulesList> GetUserRulesAsync(ulong userId, CancellationToken cancellationToken = default) {
            return await Get<UserRulesList>($"{_options.URLPostSettings}/users/{userId}/settings/user_rules", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ulong> AddUserRuleAsync(ulong userId, UserRule userRule, CancellationToken cancellationToken = default) {
            if (userRule is null) {
                throw new ArgumentNullException(nameof(userRule));
            }
            var result = await Post<UserRuleAddResponse>($"{_options.URLPostSettings}/users/{userId}/settings/user_rules", userRule, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.ruleId;
        }

        public async Task DeleteUserRuleAsync(ulong userId, ulong ruleId, CancellationToken cancellationToken = default) {
            await Delete($"{_options.URLPostSettings}/users/{userId}/settings/user_rules/{ruleId}", cancellationToken).ConfigureAwait(false);
        }
    }
}
