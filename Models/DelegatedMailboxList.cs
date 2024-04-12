using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    public class DelegatedMailboxList {
        [JsonPropertyName("resources")]
        public List<DelegatedMailbox> DelegatedMailboxes { get; set; }
    }
}
