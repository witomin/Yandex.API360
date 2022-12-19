namespace Yandex.API360.Models {
    public class DeletedMember : Member {
        /// <summary>
        /// Признак удаления участника: true — удалён; false — не удалён.
        /// </summary>
        public bool deleted { get; set; }
    }
}
