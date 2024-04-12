using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    /// <summary>
    /// Тип прав на доступ к почтовому ящику
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum RightType {
        /// <summary>
        /// право на чтение почты и управление настройками ящика по протоколу IMAP, возможности отправлять письма это право не дает
        /// </summary>
        [EnumMember(Value = "imap_full_access")]
        [Description("Чтение почты и управление настройками ящика")]
        ImapFullAccess,
        /// <summary>
        /// возможность отправлять письма из ящика по протоколу SMTP от своего имени: в поле "От кого" в письме будет указан адрес настоящего отправителя
        /// </summary>
        [EnumMember(Value = "send_on_behalf")]
        [Description("Отправка писем от своего имени")]
        SendOnBehalf,
        /// <summary>
        /// возможность отправлять письма по протоколу SMTP от имени владельца ящика: в поле "От кого" в письме будет указан адрес владелеца
        /// </summary>
        [EnumMember(Value = "send_as")]
        [Description("Отправка писем от имени владельца ящика")]
        SendAs
    }
}
