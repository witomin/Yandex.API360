using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public interface IPostSettingsClient {
        /// <summary>
        /// Получить почтовый адрес, с которого отправляются письма по умолчанию, и настройки подписей сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<UserPersonalSettings> GetAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Установить почтовый адрес сотрудника, с которого отправляются письма по умолчанию, и настройки его подписей
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="UserSettings">Настройки пользователя</param>
        /// <returns></returns>
        public Task<UserPersonalSettings> SetAsync(ulong userId, UserPersonalSettings UserSettings, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить статус автоматического сбора контактов
        /// Метод позволяет просмотреть, включено ли автоматическое формирование адресной книги сотрудника из адресов исходящей почты.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<CollectAddressStatus> GetСollectAddressesAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        ///  Установить статус автоматического сбора контактов
        ///  Метод позволяет управлять опцией автоматического формирования адресной книги сотрудника из адресов исходящей почты.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="collectAddresses">Автоматически собирать контакты</param>
        /// <returns></returns>
        public Task<CollectAddressStatus> SetСollectAddressesAsync(ulong userId, CollectAddressStatus collectAddresses, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить правила автоответа и пересылки
        /// Метод позволяет просмотреть правила автоответа и пересылки писем, настроенные для сотрудника администратором. Правила, созданные сотрудником, не выводятся.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public Task<UserRulesList> GetUserRulesAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Создать правило автоответа или пересылки
        /// Метод позволяет создать правило автоответа или пересылки писем для сотрудника. Возможность пересылки есть только на домены, которые принадлежат выбранной организации. Подтверждение получения пересылки при создании такого правила не требуется.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userRule">правило автоответа или пересылки</param>
        /// <returns></returns>
        public Task<ulong> AddUserRuleAsync(ulong userId, UserRule userRule, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить правило автоответа или пересылки
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="ruleId">Идентификатор правила</param>
        /// <returns></returns>
        public Task DeleteUserRuleAsync(ulong userId, ulong ruleId, CancellationToken cancellationToken = default);
    }
}
