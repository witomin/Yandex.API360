namespace Yandex.API360.Models {
    class RemovedAlias {
        /// <summary>
        /// Алиас почтового ящика сотрудника
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// Признак удаления: true — удален; false — не удален
        /// </summary>
        public bool removed { get; set; }
    }
}
