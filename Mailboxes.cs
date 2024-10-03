using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Посмотреть список делегированных ящиков
        /// </summary>
        /// <returns>Возвращает список делегированных почтовых ящиков в организации</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<object>> GetDelegatedMailboxesAsync() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Посмотреть список общих ящиков
        /// </summary>
        /// <returns>Возвращает список общих почтовых ящиков в организации</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<object>> GetMailboxesAsync() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Создать общий ящик
        /// </summary>
        /// <param name="email">Адрес электронной почты общего ящика</param>
        /// <param name="name">Имя общего ящика</param>
        /// <param name="description">Описание</param>
        /// <returns>Идентификатор созданного общего почтового ящика</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ulong> AddMailboxAsync(string email, string name, string description) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Посмотреть информацию об общем ящике
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <returns>Информация об общем ящике</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<object> GetMailboxInfoAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Изменить данные общего ящика
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <param name="name">Имя</param>
        /// <param name="description">Описание</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ulong> SetMailboxInfoAsync(ulong id, string name, string description) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Удалить общий ящик
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteMailboxAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Посмотреть список сотрудников, имеющих доступ к ящику
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика, права доступа к которому необходимо проверить</param>
        /// <returns>Возвращает список сотрудников, у которых есть права доступа к почтовому ящику</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<object>> GetActorsFromMailboxAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Посмотреть список ящиков, доступных сотруднику
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Возвращает список почтовых ящиков (общих и делегированных), к которым у сотрудника есть права доступа</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<object>> GetMailboxesFromUserAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Разрешить делегирование ящика
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика. Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Идентификатор почтового ящика, разрешение на делегирование которого предоставлено</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ulong> DelegateMailboxAllowAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Запретить делегирование ящика
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика. Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Идентификатор почтового ящика, разрешение на делегирование которого отзвать</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ulong> DelegateMailboxDeniedAsync(ulong id) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Изменить права доступа к ящику. Предоставляет или изменяет права доступа сотрудника к делегированному или общему почтовому ящику. Чтобы ящик другого сотрудника стал делегированным и к нему можно было получить доступ, сначала предоставьте разрешение на его делегирование.
        /// </summary>
        /// <param name="resourceId">Идентификатор почтового ящика, права доступа к которому необходимо предоставить или изменить</param>
        /// <param name="actorId">Идентификатор сотрудника, для которого настраивается доступ</param>
        /// <param name="notify">Кому необходимо отправить письмо-уведомление об изменении прав доступа к ящику</param>
        /// <param name="roles">Список прав доступа</param>
        /// <returns>Возвращает идентификатор задачи, по которому можно проверить состояние ее выполнения</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> SetMailboxRulesAsync(ulong resourceId, ulong actorId, List<object> roles, string notify = "all") {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Проверить статус задачи на изменение прав доступа
        /// </summary>
        /// <param name="id">Идентификатор задачи на управление правами доступа. Возвращается в ответе на запрос на изменение или на удаление прав доступа к почтовому ящику.</param>
        /// <returns>Возвращает статус задачи на управление правами доступа к делегированному ящику</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetStatusMailboxTaskAsync(ulong id) {
            throw new NotImplementedException();
        }
    }
}
