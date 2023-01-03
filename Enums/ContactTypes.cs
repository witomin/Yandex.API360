using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactTypes {
        staff,
        email,
        phone_extension,
        phone,
        site,
        icq,
        twitter,
        skype
    }
}
