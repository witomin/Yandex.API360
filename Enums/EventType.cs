using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Yandex.API360.Enums {
    /// <summary>
    /// Тип события
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum EventType {
        /// <summary>
        /// копирование к себе на Диск
        /// </summary>
        [EnumMember(Value = "fs-copy")]
        [Description("Копирование к себе на диск")]
        FsCopy,
        /// <summary>
        /// создание папки
        /// </summary>
        [EnumMember(Value = "fs-mkdir")]
        [Description("Создание папки")]
        FsMkdir,
        /// <summary>
        /// перемещение
        /// </summary>
        [EnumMember(Value = "fs-move")]
        [Description("Перемещение")]
        FsMove,
        /// <summary>
        /// публикация файла по ссылке
        /// </summary>
        [EnumMember(Value = "fs-set-public")]
        [Description("Публикация файла по ссылке")]
        FsSetPublic,
        /// <summary>
        /// загрузка файла
        /// </summary>
        [EnumMember(Value = "fs-store")]
        [Description("Загрузка файла")]
        FsStore,
        /// <summary>
        /// перенос в Корзину
        /// </summary>
        [EnumMember(Value = "fs-trash-append")]
        [Description("Перенос в корзину")]
        FsTrasHappend,
        /// <summary>
        /// удаление из Корзины
        /// </summary>
        [EnumMember(Value = "fs-trash-drop")]
        [Description("Удаление из корзины")]
        FsTrashDrop,
        /// <summary>
        /// очистка Корзины
        /// </summary>
        [EnumMember(Value = "fs-trash-drop-all")]
        [Description("Очистка корзины")]
        FsTrashDropAll,
        /// <summary>
        /// принятие приглашения
        /// </summary>
        [EnumMember(Value = "share-activate-invite")]
        [Description("Принятие приглашения")]
        ShareActivateInvite,
        /// <summary>
        /// изменение уровня общего доступа
        /// </summary>
        [EnumMember(Value = "share-change-rights")]
        [Description("Изменение уровня общего доступа")]
        ShareChangeRights,
        /// <summary>
        /// изменение уровня доступа для приглашения
        /// </summary>
        [EnumMember(Value = "share-change-invite-rights")]
        [Description("Изменение уровня доступа для приглашения")]
        ShareChangeInviteRights,
        /// <summary>
        /// общий доступ к папке
        /// </summary>
        [EnumMember(Value = "share-create-group")]
        [Description("Общий доступ к папке")]
        ShareCreateGroup,
        /// <summary>
        /// приглашение в группу
        /// </summary>
        [EnumMember(Value = "share-invite-user")]
        [Description("Приглашение в группу")]
        ShareInviteUser,
        /// <summary>
        /// удаление без переноса в Корзину (с использованием протокола WebDAV)
        /// </summary>
        [EnumMember(Value = "fs-rm")]
        [Description("Удаление без переноса в корзину")]
        FsRm
    }
}
