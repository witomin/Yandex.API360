namespace Yandex.API360.Models {
    /// <summary>
    /// ФИО сотрудника
    /// </summary>
    public class Name {
        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string first { get; set; }
        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        public string last { get; set; }
        /// <summary>
        /// Отчество сотрудника.
        /// </summary>
        public string middle { get; set; }
    }
}
