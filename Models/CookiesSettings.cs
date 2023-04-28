namespace Yandex.API360.Models {
    /// <summary>
    /// Параметры cookie
    /// </summary>
    public class CookiesSettings {
        /// <summary>
        /// Время (в секундах), по истечении которого cookie сессии пользователей завершаются
        /// </summary>
        public ulong authTTL { get; set; }
    }
}
