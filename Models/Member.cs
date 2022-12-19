using Yandex.API360.Enums;

namespace Yandex.API360.Models {
    public class Member {
        /// <summary>
        /// Тип участника
        /// </summary>
        public MemberTypes? type { get; set; }
        /// <summary>
        /// Идентификатор участника группы
        /// </summary>
        public ulong id { get; set; }
    }
}
