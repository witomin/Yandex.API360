using System.Collections.Generic;

namespace Yandex.API360.Models {
    /// <summary>
    /// Белый список
    /// </summary>
    public class WhiteList {
        /// <summary>
        /// список разрешенных IP-адресов и CIDR-подсетей.
        /// </summary>
        public List<string> allowList { get; set; }
    }
}
