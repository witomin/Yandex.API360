using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Yandex.API360.Exceptions;
using Yandex.API360.Models;


namespace Yandex.API360 {
    public class Client {
        Api360Options _options;
        HttpClient httpClient;
        public Client(Api360Options options) {
            _options = options;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Token);
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <returns></returns>
        public async Task<List<User>> GetUsers(int page = 1, int perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}?page={page}&perPage={perPage}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException(
                        message: "Response doesn't contain any content",
                        httpStatusCode: response.StatusCode
                    );
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
            var apiResponse = await response.Content.ReadFromJsonAsync<GetUsersAPIResponse>();
            return apiResponse.users;
        }
        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        /// <param name="userId">Id сотрудника</param>
        /// <returns></returns>
        public async Task<User> GetUserById(string userId) {
            if (string.IsNullOrEmpty(userId)) {
                throw new ArgumentNullException(nameof(userId));
            }
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException(
                        message: "Response doesn't contain any content",
                        httpStatusCode: response.StatusCode
                    );
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public async Task<User> AddUser(User user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var content = JsonContent.Create(user);
            var response = await httpClient.PostAsync($"{_options.URLUsers}", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException(
                        message: "Response doesn't contain any content",
                        httpStatusCode: response.StatusCode
                    );
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public async Task<User> EditUser(User user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var response = await httpClient.PatchAsJsonAsync($"{_options.URLUsers}/{user.id}", user);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException(
                        message: "Response doesn't contain any content",
                        httpStatusCode: response.StatusCode
                    );
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Добавить сотруднику алиас почтового ящика
        /// </summary>
        /// <param name="userId">Id сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<User> AddAliasToUser(string userId, string alias) {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(userId) ? nameof(userId) : null, string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }

            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}/{userId}/aliases", new { alias = alias });
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException(
                        message: "Response doesn't contain any content",
                        httpStatusCode: response.StatusCode
                    );
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}




