using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    /// <summary>
    /// Ответ API список делегированных ящиков
    /// </summary>
    public class MailboxListAPIResponse {
        /// <summary>
        /// Список почтовых ящиков, к которым сотруднику открыт доступ.
        /// </summary>
        [JsonPropertyName("resources")]
        public List<ActorListResource> Resources { get; set; }
        /// <summary>
        /// Номер страницы ответа
        /// </summary>
        [JsonPropertyName("page")]
        public long Page {  get; set; }
        /// <summary>
        /// Количество записей на одной странице ответа
        /// </summary>
        [JsonPropertyName("perPage")]
        public long PerPage { get; set; }
        /// <summary>
        /// Общее количество записей
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
