using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    /// <summary>
    /// Статус выполнения задачи
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum TaskStatus {
        /// <summary>
        /// Выполняется
        /// </summary>
        [EnumMember(Value = "running")]
        [Description("Выполняется")]
        Running,
        /// <summary>
        /// Успешно завершилась, права изменены
        /// </summary>
        [EnumMember(Value = "complete")]
        [Description("Успешно завершилась, права изменены")]
        Complet,
        /// <summary>
        /// Завершилась с ошибкой
        /// </summary>
        [EnumMember(Value = "error")]
        [Description("Завершилась с ошибкой")]
        Error
    }
}
