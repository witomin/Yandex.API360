using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Enums;
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
        #region Сотрудники
        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <returns></returns>
        public async Task<List<User>> GetUsers(int page = 1, int perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}?page={page}&perPage={perPage}");
            await CheckResponse(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetUsersModel>();
            return apiResponse.users;
        }
        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<User> GetUserById(string userId) {
            if (string.IsNullOrEmpty(userId)) {
                throw new ArgumentNullException(nameof(userId));
            }
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}");
            await CheckResponse(response);
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
            await CheckResponse(response);
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
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Добавить сотруднику алиас почтового ящика
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<User> AddAliasToUser(string userId, string alias) {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(userId) ? nameof(userId) : null, string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}/{userId}/aliases", new { alias = alias });
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Удалить у сотрудника алиас почтового ящика.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<bool> DeleteAliasFromUser(string userId, string alias) {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(userId) ? nameof(userId) : null, string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/aliases/{alias}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAliasModel>();
            return result.removed;
        }
        /// <summary>
        /// Возвращает информацию о статусе 2FA сотрудника.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<bool> GetStatus2FAUser(string userId) {
            if (string.IsNullOrEmpty(userId)) {
                throw new ArgumentNullException(nameof(userId));
            }
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}/2fa");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<Status2FA>();
            return result.has2fa;
        }
        #endregion
        #region Подразделения
        /// <summary>
        /// Добавить подразделению алиас
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <param name="alias">Алиас почтовой рассылки подразделения</param>
        /// <returns></returns>
        public async Task<User> AddAliasToDepartment(long departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}/{departmentId}/aliases", new { alias = alias });
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Удалить алиас почтовой рассылки подразделения.
        /// </summary>
        /// <param name="departmentId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<bool> DeleteAliasFromDepartment(long departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}/aliases/{alias}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAliasModel>();
            return result.removed;
        }
        /// <summary>
        /// Создать подразделение
        /// </summary>
        /// <param name="department">Новое подразделение</param>
        /// <returns></returns>
        public async Task<Department> AddDepartment(BaseDepartment department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}", department);
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Получить подразделение по ID
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public async Task<Department> GetDapartmentById(long departmentId) {
            var response = await httpClient.GetAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Получить список подразделений
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <param name="parentId">НИдентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.</param>
        /// <param name="orderBy">Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.</param>
        /// <returns></returns>
        public async Task<List<Department>> GetDepartments(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            string url = $"{_options.URLDepartments}?page={page}&perPage={perPage}" +
                $"{(parentId != null ? $"&parentId={parentId}" : string.Empty)}" +
                $"&orderBy={orderBy}";
            var response = await httpClient.GetAsync(url);
            await CheckResponse(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetDepartmentsModel>();
            return apiResponse.departments;
        }
        /// <summary>
        /// Изменить подразделение
        /// </summary>
        /// <param name="department">подразделение</param>
        /// <returns></returns>
        public async Task<Department> EditDepartment(Department department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PatchAsJsonAsync($"{_options.URLDepartments}/{department.id}", (BaseDepartment)department);
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public async Task<bool> DeleteDepartment(long departmentId) {
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAliasModel>();
            return result.removed;
        }
        #endregion
        #region Группы
        /// <summary>
        /// Получить список групп
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<List<Group>> GetGroups(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}?page={page}&perPage={perPage}");
            await CheckResponse(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GetGroupsModel>();
            return apiResponse.groups;
        }
        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="group">группа</param>
        /// <returns></returns>
        public async Task<Group> AddGroup(BaseGroup group) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}", group);
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        #endregion
        #region Private
        /// <summary>
        /// Проверить ответ API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task CheckResponse(HttpResponseMessage response) {
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException("Response doesn't contain any content", response.StatusCode);
                }
                var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                throw new APIRequestException(response.StatusCode, failedResponse);
            }
        }
        #endregion
    }
}




