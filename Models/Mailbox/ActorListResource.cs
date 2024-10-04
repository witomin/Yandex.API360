using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    public class ActorListResource {
        /// <summary>
        /// Количество сотрудников, которые имеют доступ к ящику.
        /// </summary>
        [JsonPropertyName("count")]
        public long Count { get; set; }
        /// <summary>
        /// Идентификатор почтового ящика
        /// </summary>
        [JsonPropertyName("resourceId")]
        public ulong ResourceId { get; set; }
    }
}
