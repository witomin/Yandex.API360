using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class UsersClient : APIClient, IUsersClient {
        public UsersClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<UsersList> GetListAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default) {
            return await Get<UsersList>($"{_options.URLUsers}?page={page}&perPage={perPage}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<User>> GetListAllAsync(CancellationToken cancellationToken = default) {
            var result = new List<User>();
            //пытаемся получить всех пользователей в одном запросе
            var apiResponse = await GetListAsync(1, _options.MaxResponseCount, cancellationToken).ConfigureAwait(false);
            //сохраняем, то что уже получили
            result.AddRange(apiResponse.users);
            //определяем кол-во страниц ответа
            var pages = apiResponse.pages;
            // получаем остальные страницы начиная со 2-й
            for (long i = 2; i <= pages; i++) {
                var usersList = await GetListAsync(i, _options.MaxResponseCount, cancellationToken).ConfigureAwait(false);
                result.AddRange(usersList.users);
            }
            return result;
        }


        public async Task<User> GetByIdAsync(ulong userId, CancellationToken cancellationToken = default) {
            return await Get<User>($"{_options.URLUsers}/{userId}", cancellationToken).ConfigureAwait(false);
        }

        public async Task<User> AddAsync(UserAdd user, CancellationToken cancellationToken = default) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            return await Post<User>($"{_options.URLUsers}", user, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<User> EditAsync(UserEdit user, CancellationToken cancellationToken = default) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            return await Patch<User>($"{_options.URLUsers}/{user.id}", user, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<User> EditAsync(User user, string password = default, CancellationToken cancellationToken = default) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var editUser = new UserEdit() {
                id = user.id,
                about = user.about,
                birthday = user.birthday,
                contacts = user.contacts.Select(x => new BaseContact { label = x.label, type = x.type, value = x.value }).ToList(),
                departmentId = user.departmentId,
                externalId = user.externalId,
                gender = user.gender,
                isAdmin = user.isAdmin,
                isEnabled = user.isEnabled,
                language = user.language,
                name = user.name,
                position = user.position,
                timezone = user.timezone,
                password = password
            };
            return await Patch<User>($"{_options.URLUsers}/{user.id}", editUser, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }, cancellationToken).ConfigureAwait(false);
        }

        public async Task<User> AddAliasAsync(ulong userId, string alias, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            return await Post<User>($"{_options.URLUsers}/{userId}/aliases", new { alias }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAliasAsync(ulong userId, string alias, CancellationToken cancellationToken = default) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var result = await Delete<RemovedAlias>($"{_options.URLUsers}/{userId}/aliases/{alias}", cancellationToken).ConfigureAwait(false);
            return result.removed;
        }

        public async Task<User> DeleteContactsAsync(ulong userId, CancellationToken cancellationToken = default) {
            return await Delete<User>($"{_options.URLUsers}/{userId}/contacts", cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> GetStatus2FAAsync(ulong userId, CancellationToken cancellationToken = default) {
            var result = await Get<UserStatus2FA>($"{_options.URLUsers}/{userId}/2fa", cancellationToken).ConfigureAwait(false);
            return result.has2fa;
        }

        public async Task Clear2FAAsync(ulong userId, CancellationToken cancellationToken = default) {
            await Delete($"{_options.URLUsers}/{userId}/2fa", cancellationToken).ConfigureAwait(false);
        }

        public async Task SetAvatar(ulong userId, byte[] imageData, CancellationToken cancellationToken = default) {
            if (imageData == null || imageData.Length == 0) {
                throw new ArgumentNullException(nameof(imageData));
            }
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageData), "file", "file.png");
            content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            _ = await Put<object>($"{_options.URLUsers}/{userId}/avatar", content, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task SetAvatar(ulong userId, Stream imageStream, CancellationToken cancellationToken = default) {
            var content = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(imageStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(fileStreamContent, "file", "avatar.png");
            _ = await Put<object>($"{_options.URLUsers}/{userId}/avatar", fileStreamContent, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
