using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Aa : BangumiResult
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("eps")]
        public Ep[] Eps { get; set; }
    }
}
