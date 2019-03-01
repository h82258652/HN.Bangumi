using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class CollectionOverviewItem
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public CollectionOverviewSubject[] List { get; set; }
    }
}
