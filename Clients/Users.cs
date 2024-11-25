﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class UsersClient : APIClient, IUsersClient {
        public UsersClient(Api360Options options) : base(options) { }

        public async Task<UsersList> GetListAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<UsersList>();
            return apiResponse;
        }

        public async Task<List<User>> GetListAllAsync() {
            var result = new List<User>();
            var response = await httpClient.GetAsync($"{_options.URLUsers}");
            await CheckResponseAsync(response);
            //определяем общее число пользователей в организации
            var totalUsers = (await response.Content.ReadFromJsonAsync<UsersList>()).total;
            response = await httpClient.GetAsync($"{_options.URLUsers}?page=1&perPage={totalUsers}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<UsersList>();
            //Проверяем весь ли список получен
            //как выяснилось 17.03.2023. API отдает максимум 1000 пользователей за 1 раз
            //хотя в документации об этом не сказано
            if (apiResponse.perPage == apiResponse.total) {
                result = apiResponse.users;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(apiResponse.users);
                //определяем кол-во страниц ответа
                var pages = apiResponse.pages;
                //определяем сколько максимально отдает API
                var perPageMax = apiResponse.perPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    var usersList = await GetListAsync(i, perPageMax);
                    result.AddRange(usersList.users);
                }
            }
            return result;
        }


        public async Task<User> GetByIdAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> AddAsync(UserAdd user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}", user);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> EditAsync(UserEdit user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var response = await httpClient.PatchAsJsonAsync($"{_options.URLUsers}/{user.id}", user, jsonSerializerOptions);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> EditAsync(User user, string password = default) {
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
            var response = await httpClient.PatchAsJsonAsync($"{_options.URLUsers}/{user.id}", editUser, jsonSerializerOptions);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<User> AddAliasAsync(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}/{userId}/aliases", new { alias = alias });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<bool> DeleteAliasAsync(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/aliases/{alias}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
            return result.removed;
        }

        public async Task<User> DeleteContactsAsync(ulong userId) {
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/contacts");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<bool> GetStatus2FAAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}/2fa");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<UserStatus2FA>();
            return result.has2fa;
        }

        public async Task Clear2FAAsync(ulong userId) {
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/2fa");
            await CheckResponseAsync(response);
            _ = await response.Content.ReadFromJsonAsync<object>();
        }

        public async Task SetAvatar(ulong userId, byte[] imageData) {
            if (imageData == null || imageData.Length == 0) {
                throw new ArgumentNullException(nameof(imageData));
            }
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(imageData), "file", "file.png");
            content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            var response = await httpClient.PutAsync($"{_options.URLUsers}/{userId}/avatar", content);
            await CheckResponseAsync(response);
            _ = await response.Content.ReadFromJsonAsync<object>();
        }

        public async Task SetAvatar(ulong userId, Stream imageStream) {
            var content = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(imageStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(fileStreamContent, "file", "avater.png");

            var response = await httpClient.PutAsync($"{_options.URLUsers}/{userId}/avatar", fileStreamContent);
            await CheckResponseAsync(response);
            _ = await response.Content.ReadFromJsonAsync<object>();
        }
    }
}
