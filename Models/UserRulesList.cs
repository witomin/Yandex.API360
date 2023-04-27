using System;
using System.Collections.Generic;
using System.Text;

namespace Yandex.API360.Models {
    /// <summary>
    /// Правила автоответа и пересылки писем
    /// </summary>
    public class UserRulesList {
        /// <summary>
        /// Правила автоответа
        /// </summary>
        public List<AutorepliRule> autoreplies { get; set; }
        /// <summary>
        /// Правила автоматической пересылки
        /// </summary>
        public List<ForwardRule> forwards { get; set; }
    }
}
