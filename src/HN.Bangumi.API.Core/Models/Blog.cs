using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Blog
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("replies")]
        public int Replies { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("dateline")]
        public string Dateline { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
