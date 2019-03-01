using System.Collections.Generic;
using HN.Bangumi.API.Models.Converters;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models
{
    public class Info
    {
        [JsonProperty("name_cn")]
        public string NameCn { get; set; }

        [JsonProperty("alias")]
        [JsonConverter(typeof(AliasConverter))]
        public string[] Alias { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birth")]
        public string Birth { get; set; }

        [JsonProperty("bloodtype")]
        public string BloodType { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("bwh")]
        public string Bwh { get; set; }

        [JsonProperty("source")]
        [JsonConverter(typeof(SingleItemArrayConverter))]
        public string[] Source { get; set; }

        [JsonProperty("Twitter")]
        public string Twitter { get; set; }
    }
}
