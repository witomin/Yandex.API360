using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.API360.Models {
    class RemovedDepartmentModel {
        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Признак удаления: true — удалено; false — не удалено.
        /// </summary>
        public bool removed { get; set; }
    }
}
