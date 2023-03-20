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
        /// Получить список сотрудников постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <returns></returns>
        public async Task<UsersList> GetUsersAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<UsersList>();
            return apiResponse;
        }
        /// <summary>
        /// Получить полный списко сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync() {
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
                    var usersList = await GetUsersAsync(i, perPageMax);
                    result.AddRange(usersList.users);
                }
            }
            return result;
        }

        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public async Task<User> AddUserAsync(UserAdd user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}", user);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public async Task<User> EditUserAsync(UserEdit user) {
            if (user is null) {
                throw new ArgumentNullException(nameof(user));
            }
            var response = await httpClient.PatchAsJsonAsync($"{_options.URLUsers}/{user.id}", user);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Добавить сотруднику алиас почтового ящика
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<User> AddAliasToUserAsync(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLUsers}/{userId}/aliases", new { alias = alias });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Удалить у сотрудника алиас почтового ящика.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<bool> DeleteAliasFromUserAsync(ulong userId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(nameof(alias));
            }
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/aliases/{alias}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
            return result.removed;
        }
        /// <summary>
        /// Удаляет контактную информацию сотрудника внесённую вручную.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<User> DeleteContactsFromUserAsync(ulong userId) {
            var response = await httpClient.DeleteAsync($"{_options.URLUsers}/{userId}/contacts");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Возвращает информацию о статусе 2FA сотрудника.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public async Task<bool> GetStatus2FAUserAsync(ulong userId) {
            var response = await httpClient.GetAsync($"{_options.URLUsers}/{userId}/2fa");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<UserStatus2FA>();
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
        public async Task<User> AddAliasToDepartmentAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}/{departmentId}/aliases", new { alias = alias });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        /// <summary>
        /// Удалить алиас почтовой рассылки подразделения.
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public async Task<bool> DeleteAliasFromDepartmentAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}/aliases/{alias}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
            return result.removed;
        }
        /// <summary>
        /// Создать подразделение
        /// </summary>
        /// <param name="department">Новое подразделение</param>
        /// <returns></returns>
        public async Task<Department> AddDepartmentAsync(BaseDepartment department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}", department);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Получить подразделение по ID
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public async Task<Department> GetDapartmentByIdAsync(long departmentId) {
            var response = await httpClient.GetAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Получить список подразделений постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <param name="parentId">Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.</param>
        /// <param name="orderBy">Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.</param>
        /// <returns></returns>
        public async Task<DepartmentsList> GetDepartmentsAsync(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            string url = $"{_options.URLDepartments}?page={page}&perPage={perPage}" +
                $"{(parentId != null ? $"&parentId={parentId}" : string.Empty)}" +
                $"&orderBy={orderBy}";
            var response = await httpClient.GetAsync(url);
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<DepartmentsList>();
            return apiResponse;
        }
        /// <summary>
        /// Получить полный список подразделений
        /// </summary>
        /// <param name="parentId">Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.</param>
        /// <param name="orderBy">Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.</param>
        /// <returns></returns>
        public async Task<List<Department>> GetAllDepartmentsAsync(long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            var result = new List<Department>();
            var response = await GetDepartmentsAsync(parentId: parentId, orderBy: orderBy);
            //определяем сколько всего подразделений
            var TotalDepartments = response.total;
            //пытаемся получить все подразделения в одном запросе
            response = await GetDepartmentsAsync(1, TotalDepartments, parentId, orderBy);
            //Проверяем все ли подразделения получены
            if (response.perPage == TotalDepartments) {
                result = response.departments;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(response.departments);
                //определяем кол-во страниц ответа
                var pages = response.pages;
                //определяем сколько максимально отдает API
                var perPageMax = response.perPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    response = await GetDepartmentsAsync(i, perPageMax, parentId, orderBy);
                    result.AddRange(response.departments);
                }
            }
            return result;
        }
        /// <summary>
        /// Изменить подразделение
        /// </summary>
        /// <param name="department">подразделение</param>
        /// <returns></returns>
        public async Task<Department> EditDepartmentAsync(Department department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PatchAsJsonAsync<BaseDepartment>($"{_options.URLDepartments}/{department.id}", department);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }
        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public async Task<bool> DeleteDepartmentAsync(ulong departmentId) {
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }
        #endregion
        #region Группы
        /// <summary>
        /// Получить список групп постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество групп на одной странице ответа</param>
        /// <returns></returns>
        public async Task<GroupsList> GetGroupsAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse;
        }
        /// <summary>
        /// Получить полный списко групп
        /// </summary>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGroupsAsync() {
            var response = await httpClient.GetAsync($"{_options.URLGroups}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            var totalGroups = apiResponse.total;
            response = await httpClient.GetAsync($"{_options.URLGroups}?page={1}&perPage={totalGroups}");
            await CheckResponseAsync(response);
            apiResponse = await response.Content.ReadFromJsonAsync<GroupsList>();
            return apiResponse.groups;
        }
        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="group">группа</param>
        /// <returns></returns>
        public async Task<Group> AddGroupAsync(BaseGroup group) {
            if (group is null) {
                throw new ArgumentNullException(nameof(group));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="groupId">идентификатор группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }
        /// <summary>
        /// Добавить участника в группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public async Task<bool> AddMemberToGroupAsync(long groupId, Member member) {
            if (member is null) {
                throw new ArgumentNullException(nameof(member));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLGroups}/{groupId}/members", member);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<AddedMember>();
            return result.added;
        }
        /// <summary>
        /// Удалить участника группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="member">Участник группы</param>
        /// <returns></returns>
        public async Task<bool> DeleteMemberFromGroupAsync(ulong groupId, Member member) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members/{member.type}/{member.id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DeletedMember>();
            return result.deleted;
        }
        /// <summary>
        /// Удалить всех участнков группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<MembersList> DeleteAllMembersFromGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MembersList>();
            return result;
        }
        /// <summary>
        /// Удалить всех руководителей группы
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<Group> DeleteAllManagersFromGroupAsync(ulong groupId) {
            var response = await httpClient.DeleteAsync($"{_options.URLGroups}/{groupId}/admins");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<Group>();
            return result;
        }
        /// <summary>
        /// Получить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<MembersList> GetGroupMembersAsync(ulong groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}/members");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<MembersList>();
        }
        /// <summary>
        /// Получить группу
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <returns></returns>
        public async Task<Group> GetGroupAsync(long groupId) {
            var response = await httpClient.GetAsync($"{_options.URLGroups}/{groupId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить группу
        /// </summary>
        /// <param name="group">Группа</param>
        /// <returns></returns>
        public async Task<Group> EditGroupAsync(Group group) {
            var response = await httpClient.PatchAsJsonAsync<BaseGroup>($"{_options.URLGroups}/{group.id}", group);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить руководителей группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="adminIds">Идентификаторы руководителей группы</param>
        /// <returns></returns>
        public async Task<Group> EditManagersFromGroupAsync(ulong groupId, List<string> adminIds) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/admins", new { adminIds = adminIds });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        /// <summary>
        /// Изменить список участников группы
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="members">Участники группы</param>
        /// <returns></returns>
        public async Task<Group> EditMembersFromGroupAsync(ulong groupId, List<Member> members) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLGroups}/{groupId}/members", new { members = members });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Group>();
        }
        #endregion
        #region Антиспам
        /// <summary>
        /// Получить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllowListAsync() {
            var response = await httpClient.GetAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<WhiteList>();
            return result.allowList;
        }
        /// <summary>
        /// Создать/изменить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <param name="allowlist">Список разрешенных IP-адресов и CIDR-подсетей.</param>
        /// <returns></returns>
        public async Task<object> SetAllowListAsync(List<string> allowlist) {
            var response = await httpClient.PostAsJsonAsync($"{_options.URLAntispam}", new WhiteList { allowList = allowlist });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
        /// <summary>
        /// Удалить список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        /// <returns></returns>
        public async Task<object> DeleteAllowListAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URLAntispam}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<object>();
        }
        #endregion
        #region 2FA
        /// <summary>
        /// Получить статус обязательной двухфакторной аутентификации (2FA) для пользователей домена.
        /// </summary>
        /// <returns></returns>
        public async Task<DomainStatus2FA> GetStatus2faAsync() {
            var response = await httpClient.GetAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
        /// <summary>
        /// Включить обязательную двухфакторную аутентификацию (2FA) для пользователей домена.
        /// </summary>
        /// <param name="status2FA"></param>
        /// <returns></returns>
        public async Task<DomainStatus2FA> Enable2faAsync(EnableDomainStatus2FA status2FA) {
            if (status2FA is null) {
                throw new ArgumentNullException(nameof(status2FA));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URL2fa}", status2FA);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
        /// <summary>
        /// Выключить обязательную двухфакторную аутентификацию (2FA) для пользователей домена
        /// </summary>
        /// <returns></returns>
        public async Task<DomainStatus2FA> Disable2faAsync() {
            var response = await httpClient.DeleteAsync($"{_options.URL2fa}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<DomainStatus2FA>();
        }
        #endregion
        #region Организации
        /// <summary>
        /// Получить организации постранично
        /// </summary>
        /// <param name="pageSize">Количество организаций на странице. Максимальное значение — 100. По умолчанию — 10.</param>
        /// <param name="pageToken">Токен постраничной навигации.</param>
        /// <returns></returns>
        public async Task<OrganizationList> GetOrganizationsAsync(int? pageSize = 10, string? pageToken = null) {
            var response = await httpClient.GetAsync($"{_options.URLOrg}?pageSize={pageSize}{(pageToken != null ? $"&pageToken={pageToken}" : string.Empty)}");
            await CheckResponseAsync(response);
            var organisations = await response.Content.ReadFromJsonAsync<OrganizationList>();
            return organisations;
        }
        /// <summary>
        /// Получить полный список организаций
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetAllOrganizationsAsync() {
            var result = new List<Organization>();
            var response = await GetOrganizationsAsync(_options.MaxCountOrgInResponse);
            if (string.IsNullOrEmpty(response.nextPageToken)) {
                result = response.organizations;
            }
            else {
                result.AddRange(response.organizations);
                do {
                    response = await GetOrganizationsAsync(_options.MaxCountOrgInResponse);
                    result.AddRange(response.organizations);
                }
                while (string.IsNullOrEmpty(response.nextPageToken));
            }
            return result;
        }
        #endregion
        #region Обработка писем
        /// <summary>
        /// Получить правила обработки писем
        /// </summary>
        /// <returns></returns>
        public async Task<List<Rule>> GetRulesAsync() {
            var response = await httpClient.GetAsync(_options.URLrouting);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }
        /// <summary>
        /// Задать правила обработки писем
        /// </summary>
        /// <param name="rulelist">Список правил</param>
        /// <returns></returns>
        public async Task<List<Rule>> SetRulesAsync(RulesList rulelist) {
            var response = await httpClient.PutAsJsonAsync(_options.URLrouting, rulelist);
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RulesList>();
            return result.rules;
        }
        #endregion
        #region Domains
        /// <summary>
        /// Получить список доменов постранично постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество доменов на одной странице ответа. Значение по умолчанию — 10.</param>
        /// <returns></returns>
        public async Task<DomainList> GetDomainsAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DomainList>();
            return result;
        }
        #endregion
        #region DNS
        /// <summary>
        /// Получить DNS записи домена постранично
        /// </summary>
        /// <param name="domainName">Полное доменное имя. Например example.com. Для кириллических доменов (например домен.рф) используйте кодировку Punycode.</param>
        /// <param name="page">Номер страницы ответа. Значение по умолчанию — 1.</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа. Значение по умолчанию — 50.</param>
        /// <returns></returns>
        public async Task<DNSList> GetDNSAsync(string domainName, long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLdomains}/{domainName}/dns?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<DNSList>();
            return result;
        }
        #endregion
        #region Private
        /// <summary>
        /// Проверить ответ API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task CheckResponseAsync(HttpResponseMessage response) {
            var Codes = new List<System.Net.HttpStatusCode> {
            System.Net.HttpStatusCode.Unauthorized,
            System.Net.HttpStatusCode.Forbidden,
            System.Net.HttpStatusCode.NotFound,
            System.Net.HttpStatusCode.InternalServerError
            };
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                if (response.Content is null) {
                    throw new APIRequestException("Response doesn't contain any content", response.StatusCode);
                }
                if (Codes.Contains(response.StatusCode)) {
                    var failedResponse = await response.Content.ReadFromJsonAsync<FailedAPIResponse>();
                    throw new APIRequestException(response.StatusCode, failedResponse);
                }
                throw new APIRequestException(response.ReasonPhrase, response.StatusCode);
            }
        }
        #endregion
    }
}




