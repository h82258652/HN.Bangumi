using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class EpProgress
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public EpProgressStatus Status { get; set; }
    }
}
