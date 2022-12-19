namespace Yandex.API360.Models {
    class RemovedElement {
        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Признак удаления: true — удалено; false — не удалено.
        /// </summary>
        public bool removed { get; set; }
    }
}
