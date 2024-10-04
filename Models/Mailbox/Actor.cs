using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    /// <summary>
    /// Сотрудник, у которого есть права доступа к почтовому ящику
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [JsonPropertyName("actorId")]
        public ulong Id { get; set; }
        /// <summary>
        /// Список прав доступа
        /// </summary>
        [JsonPropertyName("roles")]
        public List<RoleType> Roles { get; set; }
    }
}
