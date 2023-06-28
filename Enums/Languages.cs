using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Languages {
        ru,
        en,
        ua,
        by,
        tr
    }
}
