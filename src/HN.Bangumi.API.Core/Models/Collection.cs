using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Collection
    {
        [JsonProperty("wish")]
        public int Wish { get; set; }

        [JsonProperty("collect")]
        public int Collect { get; set; }

        [JsonProperty("doing")]
        public int Doing { get; set; }

        [JsonProperty("on_hold")]
        public int OnHold { get; set; }

        [JsonProperty("dropped")]
        public int Dropped { get; set; }
    }
}
