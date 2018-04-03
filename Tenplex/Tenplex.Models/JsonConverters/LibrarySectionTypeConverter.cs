using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Tenplex.Models.JsonConverters
{
    public class LibrarySectionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString()))
                return default(LibrarySectionType);

            var value = reader.Value.ToString();
            var type = value == "artist" ? LibrarySectionType.Artist : value == "show" ? LibrarySectionType.Show : LibrarySectionType.Movie;
            return type;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var type = (LibrarySectionType)value;
                var t = JToken.FromObject(type);
                t.WriteTo(writer);
            }

            catch
            {
                var t = JToken.FromObject(default(LibrarySectionType));
                t.WriteTo(writer);
            }
        }
    }
}
