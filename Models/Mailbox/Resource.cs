using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    public class Resource {
        /// <summary>
        /// Идентификатор почтового ящика
        /// </summary>
        [JsonPropertyName("resourceId")]
        public ulong ResourceId { get; set; }
        /// <summary>
        /// Идентификатор почтового ящика
        /// </summary>
        [JsonPropertyName("type")]
        public ResourceType Type { get; set; }
        /// <summary>
        /// Список прав доступа, предоставленных сотруднику
        /// </summary>
        [JsonPropertyName("roles")]
        public List<RoleType> Roles { get; set; }
    }
}
