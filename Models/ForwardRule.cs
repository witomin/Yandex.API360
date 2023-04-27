namespace Yandex.API360.Models {
    /// <summary>
    /// Правило автоматической пересылки
    /// </summary>
    public class ForwardRule {
        /// <summary>
        ///  Идентификатор правила
        /// </summary>
        public ulong ruleId { get; set; }
        /// <summary>
        /// Email получателя пересылаемого письма
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Название правила
        /// </summary>
        public string ruleName { get; set; }
        /// <summary>
        /// Признак того, нужно ли сохранять копию письма в ящике исходного получателя
        /// </summary>
        public bool withStore { get; set; }
    }
}