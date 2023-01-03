namespace Yandex.API360.Models {
    public class Contact: BaseContact {
        /// <summary>
        /// Признак основного контакта: true — основной; false — альтернативный
        /// </summary>
        public bool main { get; set; }
        /// <summary>
        /// Если у сотрудника есть алиас, для него автоматически создается контакт типа email: true — контакт создан на основе алиаса; false — контакт создан вручную
        /// </summary>
        public bool alias { get; set; }
        /// <summary>
        /// Признак автоматически созданного контакта: true — контакт создан автоматически; false — контакт создан вручную
        /// </summary>
        public bool synthetic { get; set; }
    }
}
