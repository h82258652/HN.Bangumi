using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class BangumiResult
    {
        [JsonProperty("code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error")]
        public string ErrorMessage { get; set; }

        [JsonProperty("request")]
        public string Request { get; set; }
    }
}
