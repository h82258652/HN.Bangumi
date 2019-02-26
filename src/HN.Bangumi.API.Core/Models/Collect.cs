using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Collect
    {
        [JsonProperty("Ee")]
        public Ee Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
