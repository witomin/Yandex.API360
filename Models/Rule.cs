using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Правило обработки писем.
    /// </summary>
    public class Rule {
        /// <summary>
        /// Описание действий, которые необходимо выполнить при срабатывании правила.
        /// </summary>
        public List<Action> actions { get; set; }
        /// <summary>
        /// JSON-описание условия (составного условия), задающее критерий соответствия письма текущему правилу.
        /// </summary>
        public dynamic condition { get; set; }
        public Scope scope { get; set; }
        /// <summary>
        /// Флаг-признак необходимости прекратить применение последующих правил при срабатывании данного.
        /// </summary>
        public bool terminal { get; set; }
    }


}
