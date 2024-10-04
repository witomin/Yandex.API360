using System.ComponentModel;
using System.Runtime.Serialization;

namespace Yandex.API360.Models.Mailbox {
    public enum ResourceType {
        /// <summary>
        /// Общий
        /// </summary>
        [EnumMember(Value = "shared")]
        [Description("Общий")]
        Shared,
        /// <summary>
        /// Делегированный
        /// </summary>
        [EnumMember(Value = "delegated")]
        [Description("Делегированный")]
        Delegated
    }
}
