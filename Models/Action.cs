using System.Text.Json.Serialization;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Описание действия, которое необходимо выполнить при срабатывании правила обработки писем
    /// </summary>
    
    public class Action {
        /// <summary>
        /// Действие
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ActionTypes action { get; set; }
        /// <summary>
        /// Данные для действия
        /// </summary>
        public ActionData? data { get; set; }
    }
    /// <summary>
    /// Данные для действия
    /// </summary>
    public class ActionData {
        /// <summary>
        /// Email для пересылки
        /// </summary>
        public string email { get; set; }
    }
}

