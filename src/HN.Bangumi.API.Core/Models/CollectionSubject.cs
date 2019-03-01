using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HN.Bangumi.API.Models
{
    public class CollectionSubject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("ep_status")]
        public int EpStatus { get; set; }

        [JsonProperty("vol_status")]
        public int VolStatus { get; set; }

        [JsonProperty("lasttouch")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastTouch { get; set; }

        [JsonProperty("subject")]
        public Subject Subject { get; set; }
    }
}
