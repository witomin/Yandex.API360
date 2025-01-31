using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    /// <summary>
    /// Внешний контакт. Короткая запись
    /// </summary>
    public class ExternalContactShort {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }
    }
}
