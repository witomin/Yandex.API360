using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;
using Yandex.API360.Models.Mailbox;

namespace Yandex.API360 {
    public class MailboxesClient : APIClient, IMailboxesClient {
        public MailboxesClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default) {
            var result = await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated?page={page}&perPage={perPage}", cancellationToken).ConfigureAwait(false);
            return result.Resources;
        }
        public async Task<List<ResourceShort>> GetDelegatedMailboxesAsync(CancellationToken cancellationToken = default) {
            var result = new List<ResourceShort>();
            //определяем общее число записей
            var totalRecords = (await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated", cancellationToken).ConfigureAwait(false)).Total;
            var apiResponse = await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/delegated?page=1&perPage={totalRecords}", cancellationToken).ConfigureAwait(false);
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
                    var recordList = await GetDelegatedMailboxesAsync(i, perPageMax, cancellationToken).ConfigureAwait(false);
                    result.AddRange(recordList);
                }
            }
            return result;
        }
        public async Task<List<ResourceShort>> GetListAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default) {
            return (await Get<MailboxListAPIResponse>($"{_options.URLMailboxManagement}/shared?page={page}&perPage={perPage}", cancellationToken).ConfigureAwait(false)).Resources;
        }

        public async Task<ulong> AddAsync(string email, string name, string description, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(email)) {
                throw new ArgumentException(nameof(email));
            }
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException(nameof(name));
            }
            if (string.IsNullOrEmpty(description)) {
                throw new ArgumentException(nameof(description));
            }
            var result = await Put<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/shared", new { email, name, description }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.ResourceId;
        }

        public async Task<MailboxInfo> GetInfoAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            return await Get<MailboxInfo>($"{_options.URLMailboxManagement}/shared/{id}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ulong> SetInfoAsync(ulong id, string name, string description, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }

            var result = await Put<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/shared/{id}", new { name, description }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.ResourceId;
        }

        public async Task DeleteAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            await Delete($"{_options.URLMailboxManagement}/shared/{id}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Models.Mailbox.Actor>> GetActorsAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await Get<ActorListAPIResponse>($"{_options.URLMailboxManagement}/actors/{id}", cancellationToken).ConfigureAwait(false);
            return result.Actors;
        }

        public async Task<List<Resource>> GetMailboxesFromUserAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await Get<ResourceListAPIResponse>($"{_options.URLMailboxManagement}/resources/{id}", cancellationToken).ConfigureAwait(false);
            return result.Resources;
        }

        public async Task<ulong> DelegateAllowAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            var result = await Put<ResourceIdAPIResponse>($"{_options.URLMailboxManagement}/delegated", new { resourceId = $"{id}" }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.ResourceId;
        }

        public async Task DelegateDeniedAsync(ulong id, CancellationToken cancellationToken = default) {
            if (id == 0) {
                throw new ArgumentException(nameof(id));
            }
            await Delete($"{_options.URLMailboxManagement}/delegated/{id}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<string> SetRulesAsync(ulong resourceId, ulong actorId, List<RoleType> roles, NotifyType notify = NotifyType.All, CancellationToken cancellationToken = default) {
            if (resourceId == 0) {
                throw new ArgumentException(nameof(resourceId));
            }
            if (actorId == 0) {
                throw new ArgumentException(nameof(actorId));
            }
            if (roles is null) {
                roles = new List<RoleType>();
            }
            var result = await Post<TaskIdAPIResponse>($"{_options.URLMailboxManagement}/set/{resourceId}?actorId={actorId}{(notify == NotifyType.All ? string.Empty : $"&notify={JsonSerializer.Serialize(notify).Trim('"')}")}", new { roles }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result.TaskId;
        }

        public async Task<Enums.TaskStatus> GetTaskStatusAsync(string taskId, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(taskId)) {
                throw new ArgumentNullException(nameof(taskId));
            }
            var result = await Get<TaskStatusResponse>($"{_options.URLMailboxManagement}/tasks/{taskId}", cancellationToken).ConfigureAwait(false);
            return result.Status;
        }
    }
}
