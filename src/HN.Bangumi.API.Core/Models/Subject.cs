using HN.Bangumi.API.Models.Converters;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Subject : BangumiResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_cn")]
        public string NameCn { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("eps")]
        [JsonConverter(typeof(TryParseConverter))]
        public Ep2[] Eps { get; set; }

        [JsonProperty("eps_count")]
        public int EpsCount { get; set; }

        [JsonProperty("air_date")]
        public string AirDate { get; set; }

        [JsonProperty("air_weekday")]
        public int AirWeekday { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("collection")]
        public Collection Collection { get; set; }

        [JsonProperty("crt")]
        public Character[] Crt { get; set; }

        [JsonProperty("staff")]
        public Staff[] Staff { get; set; }

        [JsonProperty("topic")]
        public Topic[] Topic { get; set; }

        [JsonProperty("blog")]
        public Blog[] Blog { get; set; }
    }
}
