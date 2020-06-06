using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Subs
{
    [Channel("helloworld")]
    [Subscribe(typeof(HelloWorld))]
    public class HelloWorldSub : ISubscriber<HelloWorld>
    {
        public Task CloseChannelAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }

        public Task OpenChannelAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task SubscribeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UnsubscribeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}