using System;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-correlationidobject-a-correlation-id-object
    /// </summary>
    public class CorrelationId
    {
        public CorrelationId(string location)
        {
            Location = location ??
                throw new ArgumentNullException(nameof(location));
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public string Location { get; }

    }
}