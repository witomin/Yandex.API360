namespace Yandex.API360.Models {
    /// <summary>
    /// Краткая информация о подразделении.
    /// </summary>
    public class DepartmentShort {
        /// <summary>
        /// Идентификатор подразделения
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
