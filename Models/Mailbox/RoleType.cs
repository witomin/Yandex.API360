using System.ComponentModel;
using System.Runtime.Serialization;

namespace Yandex.API360.Models.Mailbox {
    /// <summary>
    /// Возможные права доступа к общему или делегированному почтовому ящику
    /// </summary>
    public enum RoleType {
        /// <summary>
        /// полные права на ящик, к которому предоставляется доступ
        /// </summary>
        [EnumMember(Value = "shared_mailbox_owner")]
        [Description("полные права на ящик, к которому предоставляется доступ")]
        SharedMailboxOwner,
        /// <summary>
        /// управление ящиком в IMAP-клиенте
        /// </summary>
        [EnumMember(Value = "shared_mailbox_imap_admin")]
        [Description("управление ящиком в IMAP-клиенте")]
        SharedMailboxImapAdmin,
        /// <summary>
        /// отправка писем по SMTP
        /// </summary>
        [EnumMember(Value = "shared_mailbox_sender")]
        [Description("отправка писем по SMTP")]
        SharedMailboxSender,
        /// <summary>
        /// ограниченная отправка писем по SMTP
        /// </summary>
        [EnumMember(Value = "shared_mailbox_half_sender")]
        [Description("ограниченная отправка писем по SMTP")]
        SharedMailboxHalfSender,
    }
}
