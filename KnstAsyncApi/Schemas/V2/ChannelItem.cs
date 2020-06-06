using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-channelitemobject-a-channel-item-object
    /// </summary>
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