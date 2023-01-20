namespace Yandex.API360.Models {
    /// <summary>
    /// Организация в Яндекс 360 для бизнеса
    /// </summary>
    public class Organization {
        /// <summary>
        /// Идентификатор организации.
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Название организации.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Адрес почтовой рассылки организации.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Номер телефона организации.
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// Номер факса организации.
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// Язык организации.
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// Тарифный план организации.
        /// </summary>
        public string subscriptionPlan { get; set; }
    }
}
