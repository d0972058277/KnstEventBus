using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels.HelloWorlds
{
    [AsyncApi]
    [Channel("pubsub/HelloWorld")]
    [MessagePayload(typeof(HelloWorld))]
    public class HelloWorldChannel : IChannel<HelloWorld>
    {
        [Publish]
        public Task PublishAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }

        [Subscribe]
        public Task SubscribeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UnSubscribeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}