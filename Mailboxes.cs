using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yandex.API360.Models.Mailbox;
using System.Net.Http.Json;


namespace Yandex.API360 {
    public partial class Client {
        /// <summary>
        /// Посмотреть список делегированных ящиков постранично
        /// </summary>
        /// /// <param name="page">Номер страницы ответа</param>
        /// <param name="perPage">Количество записей на одной странице ответа</param>
        /// <returns>Возвращает список делегированных почтовых ящиков в организации</returns>
        public async Task<List<Resource>> GetDelegatedMailboxesAsync(long page = 1, long perPage = 10) {
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated?page={page}&perPage={perPage}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>();
            return result.Resources;
        }
        /// <summary>
        /// Получить полный список делегированных ящиков
        /// </summary>
        /// <returns></returns>
        public async Task<List<Resource>> GetDelegatedMailboxesAsync() {
            var result = new List<Resource>();
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated");
            await CheckResponseAsync(response);
            //определяем общее число записей
            var totalRecords = (await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>()).Total;
            response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/delegated?page=1&perPage={totalRecords}");
            await CheckResponseAsync(response);
            var apiResponse = await response.Content.ReadFromJsonAsync<MailboxListAPIResponse>();
            //Проверяем весь ли список получен
            if (apiResponse.PerPage == apiResponse.Total) {
                result = apiResponse.Resources;
            }
            else {
                //если API отдало не все
                //сохраняем, то что уже получили
                result.AddRange(apiResponse.Resources);
                //определяем кол-во страниц ответа
                var pages = Math.Ceiling((double)apiResponse.Total / apiResponse.PerPage);
                //определяем сколько максимально отдает API
                var perPageMax = apiResponse.PerPage;
                // получаем остальные страницы начиная со 2-й
                for (long i = 2; i <= pages; i++) {
                    var recordList = await GetDelegatedMailboxesAsync(i, perPageMax);
                    result.AddRange(recordList);
                }
            }
            return result;
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
        /// <param name="id">Идентификатор почтового ящика, права доступа к которому необходимо проверить.
        /// Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Возвращает список сотрудников, у которых есть права доступа к почтовому ящику</returns>
        public async Task<List<Actor>> GetActorsFromMailboxAsync(ulong id) {
            var response = await httpClient.GetAsync($"{_options.URLMailboxManagement}/actors/{id}");
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ActorListAPIResponse>();
            return result.Actors;
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
        public async Task<ulong> DelegateMailboxAllowAsync(ulong id) {
            var response = await httpClient.PutAsJsonAsync($"{_options.URLMailboxManagement}/delegated", new { resourceId = $"{id}" });
            await CheckResponseAsync(response);
            var result = await response.Content.ReadFromJsonAsync<ResourceIdAPIResponse>();
            return result.ResourceId;
        }
        /// <summary>
        /// Запретить делегирование ящика
        /// </summary>
        /// <param name="id">Идентификатор почтового ящика. Для делегированных ящиков идентификатор почтового ящика совпадает с идентификатором сотрудника-владельца этого ящика</param>
        /// <returns>Идентификатор почтового ящика, разрешение на делегирование которого отзвать</returns>
        public async Task DelegateMailboxDeniedAsync(ulong id) {
            var response = await httpClient.DeleteAsync($"{_options.URLMailboxManagement}/delegated/{id}");
            await CheckResponseAsync(response);
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
