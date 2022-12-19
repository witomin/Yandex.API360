using System.Collections.Generic;

namespace Yandex.API360.Models {
    public class MembersList {
        /// <summary>
        /// Подразделения
        /// </summary>
        public List<DepartmentShort> departments { get; set; }
        /// <summary>
        /// Группы
        /// </summary>
        public List<GroupShort> groups { get; set; }
        /// <summary>
        /// Сотрудники
        /// </summary>
        public List<UserShort> users { get; set; }
    }
}
