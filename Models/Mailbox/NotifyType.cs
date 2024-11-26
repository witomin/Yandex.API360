using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox {
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    /// <summary>
    /// Кому необходимо отправить письмо-уведомление об изменении прав доступа к ящику.
    /// </summary>
    public enum NotifyType {
        /// <summary>
        /// Владельцу почтового ящика и сотруднику, для которого настраивается доступ к нему
        /// </summary>
        [EnumMember(Value = "all")]
        [Description("Владельцу почтового ящика и сотруднику, для которого настраивается доступ к нему")]
        All,
        /// <summary>
        /// Только сотруднику, для которого настраивается доступ к почтовому ящику
        /// </summary>
        [EnumMember(Value = "delegates")]
        [Description("Только сотруднику, для которого настраивается доступ к почтовому ящику")]
        Delegates,
        /// <summary>
        /// Никому
        /// </summary>
        [EnumMember(Value = "none")]
        [Description("Никому")]
        None
    }
}
