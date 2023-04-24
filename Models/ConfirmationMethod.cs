namespace Yandex.API360.Models {
    /// <summary>
    /// Метод подтверждения статуса подключения домена
    /// </summary>
    public class ConfirmationMethod {
        /// <summary>
        /// Код подтверждения
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Метод подтверждения. Подробнее см. в разделе Подтвердить домен и настроить сервер Справки Яндекс 360 для бизнеса
        /// https://yandex.ru/support/business/domains/dns-editor.html
        /// </summary>
        public string method { get; set; }
    }
}
