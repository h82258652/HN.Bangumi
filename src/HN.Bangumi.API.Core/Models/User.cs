using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class User : BangumiResult
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("usergroup")]
        public int UserGroup { get; set; }
    }
}
