using System.Collections.Generic;

namespace Yandex.API360.Exceptions {
    /// <summary>
    /// Ответ API с ошибкой
    /// </summary>
    public class FailedAPIResponse {
        public int code { get; set; }
        public string message { get; set; }
        public List<string> details { get; set; }
    }
}
