using Newtonsoft.Json;
using KnstAsyncApi.Schemas.V2.Bindings.Amqp;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-channelbindingsobject-a-channel-bindings-object
    /// </summary>
    public class ChannelBindings
    {
        [JsonProperty("amqp")]
        public AmqpChannelBinding Amqp { get; set; }
    }
}