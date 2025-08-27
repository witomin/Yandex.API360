using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Enums;
using Yandex.API360.Models;

namespace Yandex.API360 {
    /// <summary>
    /// Endpoints для работы с подразделениями
    /// </summary>
    public interface IDepartmentsClient {
        /// <summary>
        /// Добавить подразделению алиас
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <param name="alias">Алиас почтовой рассылки подразделения</param>
        /// <returns></returns>
        public Task<User> AddAliasAsync(ulong departmentId, string alias, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить алиас почтовой рассылки подразделения.
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public Task<bool> DeleteAliasAsync(ulong departmentId, string alias, CancellationToken cancellationToken = default);
        /// <summary>
        /// Создать подразделение
        /// </summary>
        /// <param name="department">Новое подразделение</param>
        /// <returns></returns>
        public Task<Department> AddAsync(BaseDepartment department, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить подразделение по ID
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public Task<Department> GetByIdAsync(ulong departmentId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить список подразделений постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <param name="parentId">Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.</param>
        /// <param name="orderBy">Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.</param>
        /// <returns></returns>
        public Task<DepartmentsList> GetListAsync(long page = 1, long perPage = 10, long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить полный список подразделений
        /// </summary>
        /// <param name="parentId">Идентификатор родительского подразделения. Если не указан, то выводятся все подразделения организации.</param>
        /// <param name="orderBy">Вид сортировки. id: По идентификатору.name: По названию.Значение по умолчанию: id.</param>
        /// <returns></returns>
        public Task<List<Department>> GetListAllAsync(long? parentId = default, DepartmentsOrderBy orderBy = DepartmentsOrderBy.id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменить подразделение
        /// </summary>
        /// <param name="department">подразделение</param>
        /// <returns></returns>
        public Task<Department> EditAsync(Department department, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <param name="departmentId">Идентификатор подразделения</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(ulong departmentId, CancellationToken cancellationToken = default);
    }
}
