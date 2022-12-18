namespace Yandex.API360.Models {
    /// <summary>
    /// Подразделение
    /// </summary>
    public class BaseDepartment {
        private ulong? _headId;
        /// <summary>
        /// Описание подразделения
        /// </summary>
        public string? description { get; set; }
        /// <summary>
        /// Произвольный внешний идентификатор подразделения
        /// </summary>
        public string? externalId { get; set; }
        /// <summary>
        /// Идентификатор сотрудника-руководителя отдела
        /// </summary>
        public ulong? headId {
            get {
                return _headId;
            }
            set {
                if (value == 0)
                    _headId = null;
                else
                    _headId = value;
            }
        }
        /// <summary>
        /// Имя почтовой рассылки подразделения. Например, для адреса new-department@ваш-домен.ru имя почтовой рассылки — это new-department
        /// </summary>
        public string? label { get; set; }
        /// <summary>
        /// Название подразделения
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public long parentId { get; set; }
    }

}
