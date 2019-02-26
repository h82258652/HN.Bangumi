using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Avatar
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }
    }
}
