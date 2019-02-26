using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Images
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("grid")]
        public string Grid { get; set; }
    }
}
