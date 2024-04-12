using System.Collections.Generic;
using System.Text.Json.Serialization;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Почтовый ящик, к которому открыт доступ
    /// </summary>
    public class DelegatedMailbox {
        /// <summary>
        /// Идентификатор владельца почтового ящика
        /// </summary>
        [JsonPropertyName("resourceId")]
        public ulong Id { get; set; }
        /// <summary>
        /// Список прав доступа, предоставленных сотруднику
        /// </summary>
        [JsonPropertyName("rights")]
        public List<RightType> Rights { get; set; }
    }
}
