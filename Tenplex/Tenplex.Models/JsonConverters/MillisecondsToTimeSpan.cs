using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Tenplex.Models.JsonConverters
{
    public class MillisecondsToTimeSpan : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString()))
                return new TimeSpan();

            var milliseconds = int.Parse(reader.Value.ToString());
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var timeSpan = (TimeSpan)value;
                var t = JToken.FromObject(timeSpan.Milliseconds);
                t.WriteTo(writer);
            }

            catch
            {
                var t = JToken.FromObject(new DateTime());
                t.WriteTo(writer);
            }
        }
    }
}
