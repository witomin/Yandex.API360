namespace Yandex.API360.Models {
    /// <summary>
    /// Правила автоответа и пересылки писем
    /// </summary>
    public class UserRule{
        /// <summary>
        /// Правило автоответа
        /// </summary>
        public AutorepliRule autoreply { get; set; }
        /// <summary>
        /// Правило автоматической пересылки
        /// </summary>
        public ForwardRule forward { get; set; }
    }
}
