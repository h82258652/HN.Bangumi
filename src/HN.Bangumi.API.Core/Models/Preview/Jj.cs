using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Preview
{
    public class Jj
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("subject")]
        public Subject Subject { get; set; }
    }
}
