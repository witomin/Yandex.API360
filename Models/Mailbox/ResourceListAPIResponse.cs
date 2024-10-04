using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox
{
    public class ResourceListAPIResponse
    {
        [JsonPropertyName("resources")]
        /// <summary>
        /// Список почтовых ящиков, к которым сотруднику открыт доступ
        /// </summary>
        public List<Resource> Resources { get; set; }
    }
}