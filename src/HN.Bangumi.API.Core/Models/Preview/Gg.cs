using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Preview
{
    public class Gg
    {
        [JsonProperty("status")]
        public Hh Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public Jj[] List { get; set; }
    }
}
