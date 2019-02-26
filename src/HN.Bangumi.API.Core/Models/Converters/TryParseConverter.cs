using System;
using Newtonsoft.Json;

namespace HN.Bangumi.API.Models.Converters
{
    public class TryParseConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return serializer.Deserialize(reader, objectType);
            }
            catch (JsonSerializationException)
            {
                return existingValue;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
