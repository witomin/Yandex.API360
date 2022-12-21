namespace Yandex.API360.Models {
    /// <summary>
    /// Статус 2FA пользователя
    /// </summary>
    class UserStatus2FA {
        /// <summary>
        /// Признак включения у сотрудника 2FA.
        /// </summary>
        public bool has2fa { get; set; }
        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public string userId { get; set; }
    }
}
