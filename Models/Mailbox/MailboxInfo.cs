using System;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    /// <summary>
    /// Информация об общем ящике
    /// </summary>
    public class MailboxInfo {
        /// <summary>
        /// Идентификатор общего ящика
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }
        /// <summary>
        /// Адрес электронной почты общего ящика
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
        /// <summary>
        /// Имя общего ящика
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
        /// <summary>
        /// Дата и время создания общего ящика
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Дата и время изменения общего ящика
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
