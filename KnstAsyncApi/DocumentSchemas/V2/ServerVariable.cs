using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2 {
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