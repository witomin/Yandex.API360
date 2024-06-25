using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yandex.API360.Exceptions {
    /// <summary>
    /// Ответ API с ошибкой
    /// </summary>
    public class FailedAPIResponse {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("details")]
        public List<Detail> Details { get; set; }
        public override string ToString() {
            return JsonSerializer.Serialize(this); 
        }
    }

    public class Detail {
        [JsonPropertyName("@type")]
        public string Type { get; set; }
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
        [JsonPropertyName("servingData")]
        public string ServingData { get; set; }

    }
}
