using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models.Mailbox;

namespace Yandex.API360 {
    /// <summary>
    /// Enpoints для работы с общими ящиками
    /// </summary>
    public interface IMailboxesClient {
        /// <summary>
        /// Посмотреть список делегированных ящиков постранично
        /// </summary>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество записей на одной странице ответа</param>
        /// <returns>Возвращает список делегированных почтовых ящиков в организации</returns>
        public Task<List<ResourceShort>> GetDelegatedMailboxesAsync(long page = 1, long perPage = 10);
        /// <summary>
        /// Получить полный список делегированных ящиков
        /// </summary>
        /// <returns></returns>
        public Task<List<ResourceShort>> GetDelegatedMailboxesAsync();
        /// <summary>
        /// Посмотреть список общих ящиков
        /// </summary>
        /// <returns>Возвращает список общих почтовых ящиков в организации</returns>
        /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество записей на одной странице ответа</param>
        public Task<List<ResourceShort>> GetMailboxesAsync(long page = 1, long perPage = 10);
        /// <summary>
        /// Создать общий ящик
        /// </summary>
        /// <param name="email">Адрес электронной почты общего ящика</param>
        /// <param name="name">Имя общего ящика</param>
        /// <param name="description">Описание</param>
        /// <returns>Идентификатор созданного общего почтового ящика</returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<ulong> AddAsync(string email, string name, string description);
        /// <summary>
        /// Посмотреть информацию об общем ящике
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <returns>Информация об общем ящике</returns>
        public Task<MailboxInfo> GetInfoAsync(ulong id);
        /// <summary>
        /// Изменить данные общего ящика
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <param name="name">Имя</param>
        /// <param name="description">Описание</param>
        /// <returns></returns>
        public Task<ulong> SetInfoAsync(ulong id, string name, string description);
        /// <summary>
        /// Удалить общий ящик
        /// </summary>
        /// <param name="id">Идентификатор общего почтового ящика</param>
        /// <returns></returns>
        public Task DeleteAsync(ulong id);
        /// <summary>
        /// Посмотреть список сотрудников, имеющих доступ к ящику
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика, права доступа к которому необходимо проверить.
        /// Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Возвращает список сотрудников, у которых есть права доступа к почтовому ящику</returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<List<Models.Mailbox.Actor>> GetActorsAsync(ulong id);
        /// <summary>
        /// Посмотреть список ящиков, доступных сотруднику
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Возвращает список почтовых ящиков (общих и делегированных), к которым у сотрудника есть права доступа</returns>
        /// <exception cref="ArgumentException"></exception>
        public  Task<List<Resource>> GetMailboxesFromUserAsync(ulong id);
        /// <summary>
        /// Разрешить делегирование ящика
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика. Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Идентификатор почтового ящика, разрешение на делегирование которого предоставлено</returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<ulong> DelegateMailboxAllowAsync(ulong id);
        /// <summary>
        /// Запретить делегирование ящика
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика. Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Идентификатор почтового ящика, разрешение на делегирование которого отзвать</returns>
        /// <exception cref="ArgumentException"></exception>
        public Task DelegateMailboxDeniedAsync(ulong id);
        /// <summary>
        /// Изменить права доступа к ящику. Предоставляет или изменяет права доступа сотрудника к делегированному или общему почтовому ящику. Чтобы ящик другого сотрудника стал делегированным и к нему можно было получить доступ, сначала предоставьте разрешение на его делегирование.
        /// </summary>
        /// <param name="resourceId">Идентификатор почтового ящика, права доступа к которому необходимо предоставить или изменить</param>
        /// <param name="actorId">Идентификатор сотрудника, для которого настраивается доступ</param>
        /// <param name="notify">Кому необходимо отправить письмо-уведомление об изменении прав доступа к ящику</param>
        /// <param name="roles">Список прав доступа</param>
        /// <returns>Возвращает идентификатор задачи, по которому можно проверить состояние ее выполнения</returns>
        /// <exception cref="ArgumentException"></exception>
        public Task<string> SetMailboxRulesAsync(ulong resourceId, ulong actorId, List<RoleType> roles, NotifyType notify = NotifyType.All);
        /// <summary>
        /// Проверить статус задачи на изменение прав доступа
        /// </summary>
        /// <param name="id">Идентификатор задачи на управление правами доступа. Возвращается в ответе на запрос на изменение или на удаление прав доступа к почтовому ящику.</param>
        /// <returns>Возвращает статус задачи на управление правами доступа к делегированному ящику</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Enums.TaskStatus> GetStatusMailboxTaskAsync(string taskId);
    }
}
