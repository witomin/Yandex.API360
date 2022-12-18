using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MemberTypes {
        user = 1,
        group = 2,
        department = 3
    }
}
