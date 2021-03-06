using System;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-a2sidstring-a-identifier
    /// </summary>
    public class Identifier
    {
        private readonly Uri value;

        public Identifier(string identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));

            value = new Uri(identifier);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Identifier);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType != JsonToken.String) return null;

                var text = reader.Value.ToString();
                return new Identifier(text);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var identifier = value as Identifier;
                serializer.Serialize(writer, identifier.ToString());
            }
        }
    }
}