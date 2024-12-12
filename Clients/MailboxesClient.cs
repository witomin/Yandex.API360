using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;
using Yandex.API360.Models.Mailbox;

namespace Yandex.API360 {
    public class MailboxesClient : APIClient, IMailboxesClient {
        public MailboxesClient(Api360Options options) : base(options) { }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync(long page = 1, long perPage = 10) {
            var result = await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated?page={page}&perPage={perPage}");
            return result.Resources;
        }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync() {
            var result = new List<ResourceShort>();
            //определяем общее число записей
            var totalRecords = (await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated")).Total;
            var apiResponse = await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated?page=1&perPage={totalRecords}");
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
        public async Task<List<ResourceShort>> GetListAsync(long page = 1, long perPage = 10) {
            return (await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/shared?page={page}&perPage={perPage}")).Resources;
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
            var result = await PutAsJson<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/shared", new { email, name, description });
            return result.ResourceId;
        }

        public async Task<MailboxInfo> GetInfoAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            return await Get<MailboxInfo>($"{_options.URLMailboxManagement}/shared/{id}");
        }

        public async Task<ulong> SetInfoAsync(ulong id, string name, string description) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }

            var result = await PutAsJson<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/shared/{id}", new { name, description });
            return result.ResourceId;
        }

        public async Task DeleteAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            await Delete($"{_options.URLMailboxManagement}/shared/{id}");
        }

        public async Task<List<Models.Mailbox.Actor>> GetActorsAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await Get<ActorListAPIResponse>($"{_options.URLMailboxManagement}/actors/{id}");
            return result.Actors;
        }

        public async Task<List<Resource>> GetMailboxesFromUserAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await Get<ResourceListAPIResponse>($"{_options.URLMailboxManagement}/resources/{id}");
            return result.Resources;
        }

        public async Task<ulong> DelegateAllowAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await PutAsJson<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/delegated", new { resourceId = $"{id}" });
            return result.ResourceId;
        }

        public async Task DelegateDeniedAsync(ulong id) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            await Delete($"{_options.URLMailboxManagement}/delegated/{id}");
        }

        public async Task<string> SetRulesAsync(ulong resourceId, ulong actorId, List<RoleType> roles, NotifyType notify = NotifyType.All) {
            if (resourceId == 0) {
                throw new ArgumentException(nameof(resourceId));
            }
            if (actorId == 0) {
                throw new ArgumentException(nameof(actorId));
            }
            if (roles is null) {
                roles = new List<RoleType>();
            }
            var result = await PostAsJson<TaskIdAPIResponse>($"{_options.URLMailboxManagement}/set/{resourceId}?actorId={actorId}{(notify == NotifyType.All ? string.Empty : $"&notify={JsonSerializer.Serialize(notify).Trim('"')}")}", new { roles });
            return result.TaskId;
        }

        public async Task<Enums.TaskStatus> GetTaskStatusAsync(string taskId) {
            if (string.IsNullOrEmpty(taskId)) {
                throw new ArgumentNullException(nameof(taskId));
            }
            var result = await Get<TaskStatusResponse>($"{_options.URLMailboxManagement}/tasks/{taskId}");
            return result.Status;
        }
    }
}
