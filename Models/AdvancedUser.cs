namespace Yandex.API360.Models {
    /// <summary>
    /// Расширенная информация сотрудника
    /// </summary>
    public class AdvancedUser : User {
        /// <summary>
        /// Пароль сотрудника
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Обязательность изменения пароля при первом входе: true — обязательно; false — необязательно.
        /// </summary>
        public bool passwordChangeRequired { get; set; }
    }
}
