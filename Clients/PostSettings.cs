using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Получить почтовый адрес, с которого отправляются письма по умолчанию, и настройки подписей сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<UserPersonalSettings> GetUserPersonalSettingsAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserPersonalSettings>();
        }
        /// <summary>
        /// Установить почтовый адрес сотрудника, с которого отправляются письма по умолчанию, и настройки его подписей
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="UserSettings">Настройки пользователя</param>
        /// <returns></returns>
        public async Task<UserPersonalSettings> SetUserPersonalSettingsAsync(ulong userId, UserPersonalSettings UserSettings) {
            if (UserSettings is null) {
                throw new ArgumentNullException(nameof(UserSettings));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", UserSettings);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserPersonalSettings>();
        }
        /// <summary>
        /// Получить статус автоматического сбора контактов
        /// Метод позволяет просмотреть, включено ли автоматическое формирование адресной книги сотрудника из адресов исходящей почты.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<CollectAddressStatus> GetСollectAddressesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/address_book");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<CollectAddressStatus>();
        }
        /// <summary>
        ///  Установить статус автоматического сбора контактов
        ///  Метод позволяет управлять опцией автоматического формирования адресной книги сотрудника из адресов исходящей почты.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="collectAddresses">Автоматически собирать контакты</param>
        /// <returns></returns>
        public async Task<CollectAddressStatus> SetСollectAddressesAsync(ulong userId, CollectAddressStatus collectAddresses) {
            if (collectAddresses is null) {
                throw new ArgumentNullException(nameof(collectAddresses));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/address_book", collectAddresses);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<CollectAddressStatus>();
        }
        /// <summary>
        /// Получить правила автоответа и пересылки
        /// Метод позволяет просмотреть правила автоответа и пересылки писем, настроенные для сотрудника администратором. Правила, созданные сотрудником, не выводятся.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<UserRulesList> GetUserRulesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<UserRulesList>();
        }
        /// <summary>
        /// Создать правило автоответа или пересылки
        /// Метод позволяет создать правило автоответа или пересылки писем для сотрудника. Возможность пересылки есть только на домены, которые принадлежат выбранной организации. Подтверждение получения пересылки при создании такого правила не требуется.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRule">правило автоответа или пересылки</param>
        /// <returns></returns>
        public async Task<ulong> AddUserRuleAsync(ulong userId, UserRule userRule) {
            if (userRule is null) {
                throw new ArgumentNullException(nameof(userRule));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules", userRule);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<UserRuleAddResponse>();
            return result.ruleId;
        }
        /// <summary>
        /// Удалить правило автоответа или пересылки
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="ruleId">Идентификатор правила</param>
        /// <returns></returns>
        public async Task DeleteUserRuleAsync(ulong userId, ulong ruleId) {
            var response = await httpClient.DeleteAsync($"{_options.URLPostSettings}/users/{userId}/settings/user_rules/{ruleId}");
            await CheckResponseAsync(response);
        }
    }
}
