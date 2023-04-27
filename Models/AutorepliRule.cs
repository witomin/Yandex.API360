namespace Yandex.API360.Models {
    /// <summary>
    /// Правило автоответа
    /// </summary>
    public class AutorepliRule {
        /// <summary>
        ///  Идентификатор правила
        /// </summary>
        public ulong ruleId { get; set; }
        /// <summary>
        /// Название правила
        /// </summary>
        public string ruleName { get; set; }
        /// <summary>
        /// Текст автоответа
        /// </summary>
        public string text { get; set; }
    }
}