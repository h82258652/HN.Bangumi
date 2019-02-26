using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Weekday
    {
        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("cn")]
        public string Cn { get; set; }

        [JsonProperty("ja")]
        public string Ja { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
