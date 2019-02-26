using HN.Bangumi.API.Models.Preview;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Calendar
    {
        [JsonProperty("weekday")]
        public Weekday Weekday { get; set; }

        [JsonProperty("items")]
        public Subject[] Items { get; set; }
    }
}
