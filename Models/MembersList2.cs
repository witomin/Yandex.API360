using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    public class MembersList2 {
        /// <summary>
        /// Подразделения
        /// </summary>
        [JsonPropertyName("departments")]
        public List<DepartmentShort> Departments { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        [JsonPropertyName("groups")]
        public List<GroupShort> Groups { get; set; }
        /// <summary>
        /// Сотрудники
        /// </summary>
        [JsonPropertyName("users")]
        public List<UserShort> Users { get; set; }
        /// <summary>
        /// Внешние контакты
        /// </summary>
        [JsonPropertyName("externalContacts")]
        public List<ExternalContactShort> ExternalContacts { get; set; }
    }
}
