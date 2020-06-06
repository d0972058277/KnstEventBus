using Newtonsoft.Json;
using KnstAsyncApi.DocumentSchemas.V2.Bindings.Amqp;

namespace KnstAsyncApi.DocumentSchemas.V2
{
    public class ChannelBindings
    {
        [JsonProperty("amqp")]
        public AmqpChannelBinding Amqp { get; set; }
    }
}