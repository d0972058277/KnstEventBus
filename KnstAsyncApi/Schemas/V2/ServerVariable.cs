using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-servervariableobject-a-server-variable-object
    /// </summary>
    public class ServerVariable
    {
        [JsonProperty("enum")]
        public IList<string> Enum { get; set; }

        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("examples")]
        public IList<string> Examples { get; set; }
    }
}