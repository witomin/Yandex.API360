using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    public class ActorList {
        [JsonPropertyName("actors")]
        public List<Actor> Actors { get; set; }
    }
}
