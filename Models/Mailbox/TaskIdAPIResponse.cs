using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    /// <summary>
    /// Ответ API с идентификатором задачи
    /// </summary>
    public class TaskIdAPIResponse {
        [JsonPropertyName("resourceId")]
        public string TaskId { get; set; }
    }
}
