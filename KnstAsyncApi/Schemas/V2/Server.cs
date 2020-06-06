using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-serverobject-a-server-object
    /// </summary>
    public class Server
    {
        public Server(string url, string protocol)
        {
            Url = url;
            Protocol = protocol;
        }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("protocol")]
        public string Protocol { get; }

        [JsonProperty("protocolVersion")]
        public string ProtocolVersion { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IDictionary<string, ServerVariable> Variables { get; set; }

        [JsonProperty("security")]
        public IList<SecurityRequirement> Security { get; set; }

        [JsonProperty("bindings")]
        public ServerBindings Bindings { get; set; }
    }
}