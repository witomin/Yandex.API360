using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Yandex.API360.Models {
    [Obsolete("Не поддерживается API Яндекс360 с 1 ноября 2024 г."/*, true*/)]
    public class ActorList {
        [JsonPropertyName("actors")]
        public List<Actor> Actors { get; set; }
    }
}
