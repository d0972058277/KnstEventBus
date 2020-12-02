using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus.Channels;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/HelloWorld")]
    public class HelloWorldChannel : IPubChannel<HelloWorld>, ISubChannel
    {
        [Publish]
        [Message(typeof(HelloWorld))]
        public Task PublishAsync(HelloWorld @event) => Task.CompletedTask;

        [Subscribe]
        [Message(typeof(HelloWorld))]
        public Task SubscribeAsync() => Task.CompletedTask;

        public Task UnSubscribeAsync() => Task.CompletedTask;
    }
}