using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class EpProgressStatus
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("css_name")]
        public string CssName { get; set; }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }

        [JsonProperty("cn_name")]
        public string CnName { get; set; }
    }
}
