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
        public async Task<Signatures> GetUserSignaturesAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Signatures>();
        }
        /// <summary>
        /// Установить почтовый адрес сотрудника, с которого отправляются письма по умолчанию, и настройки его подписей
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="signatures"></param>
        /// <returns></returns>
        public async Task<Signatures> SetUserSignaturesAsync(ulong userId, Signatures signatures) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/sender_info", signatures);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Signatures>();
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
            var response = await httpClient.PostAsJsonAsync($"{_options.URLPostSettings}/users/{userId}/settings/address_book", collectAddresses);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<CollectAddressStatus>();
        }
    }
}
