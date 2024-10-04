using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    public class ResourceIdAPIResponse {
        [JsonPropertyName("resourceId")]
        public ulong ResourceId { get; set; }
    }
}
