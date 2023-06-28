using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    /// <summary>
    /// Пол
    /// </summary>
    public enum Gender {
        /// <summary>
        /// Мужской
        /// </summary>
        male,
        /// <summary>
        /// Женский
        /// </summary>
        female
    }
}
