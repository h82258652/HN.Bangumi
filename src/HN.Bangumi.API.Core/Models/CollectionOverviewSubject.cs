using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class CollectionOverviewSubject
    {
        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("subject")]
        public Subject Subject { get; set; }
    }
}
