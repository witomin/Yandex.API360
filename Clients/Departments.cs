using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Enums;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public partial class Client {
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
        public async Task<Department> GetDepartmentByIdAsync(ulong departmentId) {
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
    }
}
