using System;
using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    /// <summary>
    /// Событие в аудит логе Диска организации
    /// </summary>
    public class Event {
        /// <summary>
        /// Дата и время события
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Тип события
        /// </summary>
        public EventType eventType { get; set; }
        /// <summary>
        /// Дата и время последней модификации файла или папки
        /// </summary>
        public DateTime lastModificationDate { get; set; }
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public ulong orgId { get; set; }
        /// <summary>
        /// Логин владельца файла или папки. Может быть пустым, например при очистке Корзины
        /// </summary>
        public string ownerLogin { get; set; }
        /// <summary>
        /// Имя владельца файла или папки. Может быть пустым, например при очистке Корзины
        /// </summary>
        public string ownerName { get; set; }
        /// <summary>
        /// Идентификатор владельца файла или папки. Может быть пустым, например при очистке Корзины
        /// </summary>
        public ulong? ownerUid { get; set; }
        /// <summary>
        /// Путь расположения файла или папки.
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// Идентификатор запроса в системе. Может быть неуникальным, например при групповых операциях.
        /// </summary>
        public string requestId { get; set; }
        /// <summary>
        /// Идентификатор файла или папки. Может быть пустым, например при очистке Корзины
        /// </summary>
        public string resourceFileId { get; set; }
        /// <summary>
        /// Права доступа к папке при предоставлении доступа. Значение может быть пустым
        /// </summary>
        public string rights { get; set; }
        /// <summary>
        /// Размер файла в байтах. Может быть равен нулю, например при создании папки или очистке Корзины
        /// </summary>
        public ulong size { get; set; }
        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public string uniqId { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string userLogin { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public ulong userUid { get; set; }
        /// <summary>
        /// IP клиента
        /// </summary>
        public string clientIp { get; set; }
    }
}