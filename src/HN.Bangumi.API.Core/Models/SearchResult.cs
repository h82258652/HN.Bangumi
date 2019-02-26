using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class SearchResult : BangumiResult
    {
        [JsonProperty("results")]
        public int Results { get; set; }

        [JsonProperty("list")]
        public Item[] List { get; set; }
    }
}
