﻿using System;
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
            var apiResponse = await response.Content.ReadFromJsonAsync<UsersList>();
            return apiResponse.users;
        }
        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<User> GetUserById(ulong userId) {
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
        public async Task<User> AddAliasToUser(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
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
        public async Task<bool> DeleteAliasFromUser(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/aliases/{alias}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
            return result.removed;
        }
        /// <summary>
        /// Возвращает информацию о статусе 2FA сотрудника.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<bool> GetStatus2FAUser(ulong userId) {
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
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
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
            var apiResponse = await response.Content.ReadFromJsonAsync<DepartmentsList>();
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
            var response = await httpClient.PatchAsJsonAsync<BaseDepartment>($"{_options.URLDepartments}/{department.id}", department);
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
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
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
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
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
        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="groupId">идентификатор группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteGroup(long groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }
        /// <summary>
        /// Добавить участника в группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник</param>
        /// <returns></returns>
        public async Task<bool> AddMemberToGroup(long groupId, Member member) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}/{groupId}/members", member);
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<AddedMember>();
            return result.added;
        }
        /// <summary>
        /// Удалить участника группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteMemderFromGroup(long groupId, Member member) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<DeletedMember>();
            return result.deleted;
        }
        /// <summary>
        /// Получить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<MembersList> GetGroupMembers(long groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<MembersList>();
        }
        /// <summary>
        /// Получить группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<Group> GetGroup(long groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить группу
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task<Group> EditGroup(Group group) {
            var response = await httpClient.PatchAsJsonAsync<BaseGroup>($"{_options.URLGroups}/{group.id}", group);
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        #endregion
        #region Антиспам
        /// <summary>
        /// Получить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllowList() {
            var response = await httpClient.GetAsync($"{_options.URLAntispam}");
            await CheckResponse(response);
            var result = await response.Content.ReadFromJsonAsync<WhiteList>();
            return result.allowList;
        }
        /// <summary>
        /// Создать/изменить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <param name="allowlist">Список разрешенных IP-адресов и CIDR-подсетей.</param>
        /// <returns></returns>
        public async Task<object> SetAllowList(List<string> allowlist) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLAntispam}", new WhiteList { allowList = allowlist });
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
        /// <summary>
        /// Удалить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<object> DeleteAllowList() {
            var response = await httpClient.DeleteAsync($"{_options.URLAntispam}");
            await CheckResponse(response);
            return await response.Content.ReadFromJsonAsync<object>();
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




