using System;
using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2 {
    public class ExternalDocumentation
    {
        public ExternalDocumentation(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; }
    }
}