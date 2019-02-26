using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Preview
{
    public class Hh
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
