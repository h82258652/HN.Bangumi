using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Staff
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_cn")]
        public string NameCn { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("comment")]
        public int Comment { get; set; }

        [JsonProperty("collects")]
        public int Collects { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("jobs")]
        public string[] Jobs { get; set; }
    }
}
