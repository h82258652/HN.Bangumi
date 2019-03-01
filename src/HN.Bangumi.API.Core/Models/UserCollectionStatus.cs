using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class UserCollectionStatus
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_cn")]
        public string NameCn { get; set; }

        [JsonProperty("collects")]
        public Collect[] Collects { get; set; }
    }
}
