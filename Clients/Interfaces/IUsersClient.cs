using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360 {
    public interface IUsersClient {
        /// <summary>
        /// Получить список сотрудников постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество сотрудников на одной странице ответа</param>
        /// <returns></returns>
        public Task<UsersList> GetListAsync(long page = 1, long perPage = 10, CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить полный список сотрудников
        /// </summary>
        /// <returns></returns>
        public  Task<List<User>> GetListAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public Task<User> GetByIdAsync(ulong userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public  Task<User> AddAsync(UserAdd user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <returns></returns>
        public  Task<User> EditAsync(UserEdit user, CancellationToken cancellationToken = default);
        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="user">Сотрудник</param>
        /// <param name="password">пароль сотрудника</param>
        /// <returns></returns>
        public  Task<User> EditAsync(User user, string password = default, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавить сотруднику алиас почтового ящика
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public Task<User> AddAliasAsync(ulong userId, string alias, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удалить у сотрудника алиас почтового ящика.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="alias">Алиас</param>
        /// <returns></returns>
        public Task<bool> DeleteAliasAsync(ulong userId, string alias, CancellationToken cancellationToken = default);
        /// <summary>
        /// Удаляет контактную информацию сотрудника внесённую вручную.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public Task<User> DeleteContactsAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Возвращает информацию о статусе 2FA сотрудника.
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public Task<bool> GetStatus2FAAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Сбросить телефон для 2FA сотрудника 
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <returns></returns>
        public Task Clear2FAAsync(ulong userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Загрузить портрет сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="imageData">Данные файла изображения</param>
        /// <returns></returns>
        public Task SetAvatar(ulong userId, byte[] imageData, CancellationToken cancellationToken = default);
        /// <summary>
        /// Загрузить портрет сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор сотрудника</param>
        /// <param name="imageStream">Данные файла изображения</param>
        /// <returns></returns>
        public Task SetAvatar(ulong userId, Stream imageStream, CancellationToken cancellationToken = default);
    }
}
