using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/HelloWorld")]
    [Message(typeof(HelloWorld))]
    public class HelloWorldChannel : IChannel<HelloWorld>
    {
        [Publish]
        public Task PublishAsync(HelloWorld @event) => Task.CompletedTask;

        [Subscribe]
        public Task SubscribeAsync() => Task.CompletedTask;

        public Task UnSubscribeAsync() => Task.CompletedTask;
    }
}