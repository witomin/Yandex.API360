namespace Yandex.API360.Models {
    /// <summary>
    /// Краткая информация о группе
    /// </summary>
    public class GroupShort {
        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Количество сотрудников подразделения с учетом вложенных подразделений.
        /// </summary>
        public long membersCount { get; set; }
        /// <summary>
        /// Название подразделения
        /// </summary>
        public string name { get; set; }
    }
}
