using System.Text.Json.Serialization;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Ответ API со статусом выполенения задача
    /// </summary>
    public class TaskStatusResponse {
        /// <summary>
        /// Статус выполения задачи
        /// </summary>
        [JsonPropertyName("status")]
        public TaskStatus Status { get; set; }
    }
}
