using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2
{
    public class ChannelItem
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("subscribe")]
        public Operation Subscribe { get; set; }

        [JsonProperty("publish")]
        public Operation Publish { get; set; }

        [JsonProperty("parameters")]
        public string Parameters { get; set; }

        [JsonProperty("bindings")]
        public ChannelBindings Bindings { get; set; }
    }
}