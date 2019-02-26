using System.Collections.Generic;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Rating
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("count")]
        public Dictionary<string, int> Count { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }
}
