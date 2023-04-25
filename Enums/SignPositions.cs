using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    /// <summary>
    /// Расположение подписи
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SignPositions {
        /// <summary>
        /// Внизу всего письма
        /// </summary>
        bottom,
        /// <summary>
        /// Сразу после ответа
        /// </summary>
        under
    }
}
