using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Preview
{
    public class Xx : BangumiResult
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("private")]
        public int Private { get; set; }

        [JsonProperty("tag")]
        public string[] Tag { get; set; }

        [JsonProperty("ep_status")]
        public int EpStatus { get; set; }

        [JsonProperty("lasttouch")]
        public int LastTouch { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
