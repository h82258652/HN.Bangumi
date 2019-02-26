using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Ep
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public Bb Status { get; set; }
    }
}
