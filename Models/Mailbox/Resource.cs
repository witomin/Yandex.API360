using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    public class Resource {
        /// <summary>
        /// Количество сотрудников, которые имеют доступ к ящику.
        /// </summary>
        [JsonPropertyName("count")]
        public long Count { get; set; }
        /// <summary>
        /// Идентификатор почтового ящика
        /// </summary>
        [JsonPropertyName("page")]
        public ulong resourceId { get; set; }
    }
}
