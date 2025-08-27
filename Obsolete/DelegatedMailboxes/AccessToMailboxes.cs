using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Enums;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
        [Obsolete("Метод не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        ///Изменить права доступа
        ///Предоставляет или изменяет права доступа сотрудника к чужому почтовому ящику.
        ///Действие асинхронное: можно отправлять следующий запрос, не дожидаясь выполнения предыдущего.
        /// </summary>
        /// <param name="mailboxOwnerId">Идентификатор владельца почтового ящика, права доступа к которому необходимо предоставить или изменить</param>
        /// <param name="actorId">Идентификатор сотрудника, для которого настраивается доступ</param>
        /// <param name="rights">Список прав доступа</param>
        /// <returns>Идентификатор задачи, по которому можно проверить состояние ее выполнения.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> SetMailboxAccessRulesAsync(ulong mailboxOwnerId, ulong actorId, List<RightType> rights) {
            if (rights is null || rights.Count==0) {
                throw new ArgumentNullException(nameof(rights));
            }
            var response = await _httpClient.PostAsJsonAsync($"{_options.URLMailboxDelegation}?resourceId={mailboxOwnerId}&actorId={actorId}", new { rights = rights });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<TaskIdResponse>();
            return result.TaskId;
        }
        [Obsolete("Метод не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// Посмотреть список сотрудников, имеющих доступ к ящику
        /// </summary>
        /// <param name="mailboxOwnerId">Идентификатор владельца почтового ящика, права доступа к которому необходимо проверить</param>
        /// <returns>Cписок сотрудников, у которых есть права доступа к почтовому ящику</returns>
        public async Task<List<Actor>> GetMailboxActorsAsync(ulong mailboxOwnerId) {
            var response = await _httpClient.GetAsync($"{_options.URLMailboxDelegation}/{mailboxOwnerId}/actors");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ActorList>();
            return result.Actors;
        }
        [Obsolete("Метод не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// Посмотреть список ящиков, доступных сотруднику
        /// </summary>
        /// <param name="actorId">Идентификатор сотрудника, для которого запрашивается список доступных ящиков</param>
        /// <returns>Список почтовых ящиков, к которым у сотрудника есть права доступа</returns>
        public async Task<List<DelegatedMailbox>> GetDelegatedMailboxesAsync(ulong actorId) {
            var response = await _httpClient.GetAsync($"{_options.URLMailboxDelegation}/{actorId}/resources");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DelegatedMailboxList>();
            return result.DelegatedMailboxes;
        }
        [Obsolete("Метод не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// Проверить статус задачи
        /// </summary>
        /// <param name="taskId">Идентификатор задачи на управление правами доступа. Возвращается в ответе на запрос на изменение или на удаление прав доступа к почтовому ящику</param>
        /// <returns>Статус выполнения задачи</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Enums.TaskStatus> GetDelegationTaskStatusAsync(string taskId) {
            if (string.IsNullOrEmpty(taskId)) {
                throw new ArgumentNullException(nameof(taskId));
            }
            var response = await _httpClient.GetAsync($"{_options.URLMailboxDelegation}/tasks/{taskId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<TaskStatusResponse>();
            return result.Status;
            
        }
        [Obsolete("Метод не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
        /// <summary>
        /// Удалить права доступа сотрудника к чужому почтовому ящику
        /// </summary>
        /// <param name="mailboxOwnerId">Идентификатор владельца почтового ящика, права доступа к которому необходимо удалить</param>
        /// <param name="actorId">Идентификатор сотрудника, у которого удаляется доступ</param>
        /// <returns>Идентификатор задачи, по которому можно проверить состояние ее выполнения</returns>
        public async Task<string> DeleteMailboxAccessRulesAsync(ulong mailboxOwnerId, ulong actorId) {
            var response = await _httpClient.DeleteAsync($"{_options.URLMailboxDelegation}?resourceId={mailboxOwnerId}&actorId={actorId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<TaskIdResponse>();
            return result.TaskId;
        }
    }
}
