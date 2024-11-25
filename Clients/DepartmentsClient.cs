﻿using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Yandex.API360.Enums;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DepartmentsClient :APIClient, IDepartmentsClient {
        public DepartmentsClient (Api360Options options) : base (options) { }   

        public async Task<User> AddAliasAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}/{departmentId}/aliases", new { alias = alias });
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<bool> DeleteAliasAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}/aliases/{alias}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedAlias>();
            return result.removed;
        }

        public async Task<Department> AddAsync(BaseDepartment department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PostAsJsonAsync($"{_options.URLDepartments}", department);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<Department> GetByIdAsync(ulong departmentId) {
            var response = await httpClient.GetAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<DepartmentsList> GetListAsync(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            string url = $"{_options.URLDepartments}?page={page}&perPage={perPage}" +
                $"{(parentId != null ? $"&parentId={parentId}" : string.Empty)}" +
                $"&orderBy={orderBy}";
            var response = await httpClient.GetAsync(url);
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<DepartmentsList>();
            return apiResponse;
        }

        public async Task<List<Department>> GetListAllAsync(long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            var result = new List<Department>();
            var response = await GetListAsync(parentId: parentId, orderBy: orderBy);
            //определяем сколько всего подразделений
            var TotalDepartments = response.total;
            //пытаемся получить все подразделения в одном запросе
            response = await GetListAsync(1, TotalDepartments, parentId, orderBy);
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
                    response = await GetListAsync(i, perPageMax, parentId, orderBy);
                    result.AddRange(response.departments);
                }
            }
            return result;
        }

        public async Task<Department> EditAsync(Department department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            var response = await httpClient.PatchAsJsonAsync<BaseDepartment>($"{_options.URLDepartments}/{department.id}", department);
            await CheckResponseAsync(response);
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<bool> DeleteAsync(ulong departmentId) {
            var response = await httpClient.DeleteAsync($"{_options.URLDepartments}/{departmentId}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<RemovedElement>();
            return result.removed;
        }
    }
}
