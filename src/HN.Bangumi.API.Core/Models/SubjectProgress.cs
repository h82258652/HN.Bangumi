using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class SubjectProgress : BangumiResult
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("eps")]
        public EpProgress[] Eps { get; set; }
    }
}
