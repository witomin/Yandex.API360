using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    /// <summary>
    /// Ответ API с идентификатором задачи
    /// </summary>
    public class TaskIdResponse {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId {  get; set; }
    }
}
