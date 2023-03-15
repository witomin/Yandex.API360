using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Ответ API Просмотреть правила
    /// </summary>
    public class RulesList {
        /// <summary>
        /// Список правил
        /// </summary>
        public List<Rule> rules { get; set; }
    }
}
