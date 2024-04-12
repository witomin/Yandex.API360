using System.Collections.Generic;
using System.Text.Json.Serialization;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Сотрудник, у которых есть права доступа к почтовому ящику другого сотрудника
    /// </summary>
    public class Actor {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [JsonPropertyName("actorId")]
        public ulong Id { get; set; }
        /// <summary>
        /// Список прав доступа
        /// </summary>
        [JsonPropertyName("rights")]
        public List<RightType> Rights { get; set; }
    }
}
