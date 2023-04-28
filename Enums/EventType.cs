using System.Runtime.Serialization;

namespace Yandex.API360.Enums {
    /// <summary>
    /// Тип события
    /// </summary>
    [DataContract]
    public enum EventType {
        /// <summary>
        /// копирование к себе на Диск
        /// </summary>
        [EnumMember(Value = "fs-copy")]
        FsCopy,
        /// <summary>
        /// создание папки
        /// </summary>
        [EnumMember(Value = "fs-mkdir")]
        FsMkdir,
        /// <summary>
        /// перемещение
        /// </summary>
        [EnumMember(Value = "fs-move")]
        FsMove,
        /// <summary>
        /// публикация файла по ссылке
        /// </summary>
        [EnumMember(Value = "fs-set-public")]
        FsSetPublic,
        /// <summary>
        /// загрузка файла
        /// </summary>
        [EnumMember(Value = "fs-store")]
        FsStore,
        /// <summary>
        /// перенос в Корзину
        /// </summary>
        [EnumMember(Value = "fs-trash-append")]
        FsTrasHappend,
        /// <summary>
        /// удаление из Корзины
        /// </summary>
        [EnumMember(Value = "fs-trash-drop")]
        FsTrashDrop,
        /// <summary>
        /// очистка Корзины
        /// </summary>
        [EnumMember(Value = "fs-trash-drop")]
        FsTrashDropAll,
        /// <summary>
        /// принятие приглашения
        /// </summary>
        [EnumMember(Value = "share-activate-invite")]
        ShareActivateInvite,
        /// <summary>
        /// изменение уровня общего доступа
        /// </summary>
        [EnumMember(Value = "share-change-rights")]
        ShareChangeRights,
        /// <summary>
        /// изменение уровня доступа для приглашения
        /// </summary>
        [EnumMember(Value = "share-change-invite-rights")]
        ShareChangeInviteRights,
        /// <summary>
        /// общий доступ к папке
        /// </summary>
        [EnumMember(Value = "share-create-group")]
        ShareCreateGroup,
        /// <summary>
        /// приглашение в группу
        /// </summary>
        [EnumMember(Value = "share-invite-user")]
        ShareInviteUser,
        /// <summary>
        /// удаление без переноса в Корзину (с использованием протокола WebDAV)
        /// </summary>
        [EnumMember(Value = "fs-rm")]
        FsRm
    }
}
