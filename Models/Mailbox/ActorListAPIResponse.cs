using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models.Mailbox
{
    public class ActorListAPIResponse
    {
        [JsonPropertyName("actors")]
        public List<Actor> Actors { get; set; }
    }
}
