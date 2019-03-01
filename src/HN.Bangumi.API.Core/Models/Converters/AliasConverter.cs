using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Converters
{
    public class AliasConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var alias = serializer.Deserialize<Dictionary<string, string>>(reader);
                return alias.Values.ToArray();
            }
            catch (JsonSerializationException)
            {
                return serializer.Deserialize<string[]>(reader);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
