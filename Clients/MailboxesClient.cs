using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;
using Yandex.API360.Models.Mailbox;

namespace Yandex.API360 {
    public class MailboxesClient : APIClient, IMailboxesClient {
        public MailboxesClient(Api360Options options):base(options) { }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>();
            return result.Resources;
        }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync() {
            var result = new List<ResourceShort>();
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated");
            await CheckResponseAsync(response);
            //определяем общее число записей
            var totalRecords = (await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>()).Total;
            response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated?page=1&perPage={totalRecords}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>();
            //Проверяем весь ли список получен
            if (apiResponse.PerPage == apiResponse.Total) {
                result = apiResponse.Resources;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(apiResponse.Resources);
                //определяем кол-во страниц ответа
                var pages = Math.Ceiling((double)apiResponse.Total / apiResponse.PerPage);
                //определяем сколько максимально отдает API
                var perPageMax = apiResponse.PerPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    var recordList = await GetDelegatedMailboxesAsync(i, perPageMax);
                    result.AddRange(recordList);
                }
            }
            return result;
        }
        public async Task<List<ResourceShort>> GetMailboxesAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/shared?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>();
            return result.Resources;
        }

        public async Task<ulong> AddAsync(string email, string name, string description) {
            if (string.IsNullOrEmpty(email)) {
                throw new ArgumentException(nameof(email));
            }
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException(nameof(name));
            }
            if (string.IsNullOrEmpty(description)) {
                throw new ArgumentException(nameof(description));
            }
            var response = await httpClient.PutAsJsonAsync($"{_options.URLMailboxManagement}/shared", new { email, name, description });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ResourceIdAPIResponse>();
            return result.ResourceId;
        }

        public async Task<MailboxInfo> GetInfoAsync(ulong id) {
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/shared/{id}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<MailboxInfo>();
        }

        public async Task<ulong> SetInfoAsync(ulong id, string name, string description) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.PutAsJsonAsync($"{_options.URLMailboxManagement}/shared/{id}", new { name, description });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ResourceIdAPIResponse>();
            return result.ResourceId;
        }

        public async Task DeleteAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLMailboxManagement}/shared/{id}");
            await CheckResponseAsync(response);
        }

        public async Task<List<Models.Mailbox.Actor>> GetActorsAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/actors/{id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ActorListAPIResponse>();
            return result.Actors;
        }

        public async Task<List<Resource>> GetMailboxesFromUserAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/resources/{id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ResourceListAPIResponse>();
            return result.Resources;
        }

        public async Task<ulong> DelegateMailboxAllowAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.PutAsJsonAsync($"{_options.URLMailboxManagement}/delegated", new { resourceId = $"{id}" });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ResourceIdAPIResponse>();
            return result.ResourceId;
        }

        public async Task DelegateMailboxDeniedAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLMailboxManagement}/delegated/{id}");
            await CheckResponseAsync(response);
        }

        public async Task<string> SetRulesAsync(ulong resourceId, ulong actorId, List<RoleType> roles, NotifyType notify = NotifyType.All) {
            if (resourceId == 0) {
                throw new ArgumentException(nameof(resourceId));
            }
            if (actorId == 0) {
                throw new ArgumentException(nameof(actorId));
            }
            if (roles is null) {
                throw new ArgumentNullException(nameof(roles));
            }
            if (roles.Count == 0) {
                throw new ArgumentException(nameof(roles));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLMailboxManagement}/set/{resourceId}?actorId={actorId}&notify={notify}", new { roles });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<TaskIdAPIResponse>();
            return result.TaskId;
        }

        public async  Task<Enums.TaskStatus> GetTaskStatusAsync(string taskId) {
            if (string.IsNullOrEmpty(taskId)) {
                throw new ArgumentNullException(nameof(taskId));
            }
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/tasks/{taskId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<TaskStatusResponse>();
            return result.Status;
        }
    }
}
