using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HN.Bangumi.API.Models
{
    public class Topic
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("main_id")]
        public int MainId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("lastpost")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastPost { get; set; }

        [JsonProperty("replies")]
        public int Replies { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
