namespace Yandex.API360.Models {
    /// <summary>
    /// Краткая информация о сотруднике.
    /// </summary>
    public class UserShort {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public ulong id { get; set; }
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public Name name { get; set; }
        /// <summary>
        /// Идентификатор портрета сотрудника
        /// </summary>
        public string avatarId { get; set; }
        /// <summary>
        /// Идентификатор подразделения, в котором состоит сотрудник
        /// </summary>
        public long departmentId { get; set; }
        /// <summary>
        /// Основной адрес электронной почты сотрудника
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Пол сотрудника
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// Логин сотрудника
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string position { get; set; }
    }
}
