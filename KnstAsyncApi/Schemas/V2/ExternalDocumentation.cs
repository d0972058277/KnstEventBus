using System;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-externaldocumentationobject-a-external-documentation-object
    /// </summary>
    public class ExternalDocumentation
    {
        public ExternalDocumentation(string url)
        {
            Url = url ??
                throw new ArgumentNullException(nameof(url));
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; }
    }
}