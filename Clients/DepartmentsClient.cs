using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Enums;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public class DepartmentsClient : APIClient, IDepartmentsClient {
        public DepartmentsClient(Api360Options options, ILogger<APIClient>? logger = default) : base(options, logger) { }

        public async Task<User> AddAliasAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            return await Post<User>($"{_options.URLDepartments}/{departmentId}/aliases", new { alias });
        }

        public async Task<bool> DeleteAliasAsync(ulong departmentId, string alias) {
            if (string.IsNullOrEmpty(alias)) {
                throw new ArgumentNullException(string.IsNullOrEmpty(alias) ? nameof(alias) : null);
            }
            var result = await Delete<RemovedAlias>($"{_options.URLDepartments}/{departmentId}/aliases/{alias}");
            return result.removed;
        }

        public async Task<Department> AddAsync(BaseDepartment department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            return await Post<Department>($"{_options.URLDepartments}", department);
        }

        public async Task<Department> GetByIdAsync(ulong departmentId) {
            return await Get<Department>($"{_options.URLDepartments}/{departmentId}");
        }

        public async Task<DepartmentsList> GetListAsync(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            string url = $"{_options.URLDepartments}?page={page}&perPage={perPage}" +
                $"{(parentId != null ? $"&parentId={parentId}" : string.Empty)}" +
                $"&orderBy={orderBy}";
            return await Get<DepartmentsList>(url);
        }

        public async Task<List<Department>> GetListAllAsync(long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id) {
            var result = new List<Department>();
            //пытаемся получить все подразделения в одном запросе
            var response = await GetListAsync(1, _options.MaxResponseCount, parentId, orderBy);

            //сохраняем, то что уже получили
            result.AddRange(response.departments);
            //определяем кол-во страниц ответа
            var pages = response.pages;
            // получаем остальные страницы начиная со 2-й
            for (long i = 2; i <= pages; i++) {
                response = await GetListAsync(i, _options.MaxResponseCount, parentId, orderBy);
                result.AddRange(response.departments);
            }

            return result;
        }

        public async Task<Department> EditAsync(Department department) {
            if (department is null) {
                throw new ArgumentNullException(nameof(department));
            }
            return await Patch<Department>($"{_options.URLDepartments}/{department.id}", department);
        }

        public async Task<bool> DeleteAsync(ulong departmentId) {
            var result = await Delete<RemovedElement>($"{_options.URLDepartments}/{departmentId}");
            return result.removed;
        }
    }
}
