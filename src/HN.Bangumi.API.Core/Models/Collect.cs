using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Collect
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
