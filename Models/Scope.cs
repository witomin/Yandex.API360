using System.Text.Json.Serialization;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    public class Scope {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DirectionTypes direction { get; set; }
    }
}
