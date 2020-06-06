using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Pubs
{
    [Channel("helloworld")]
    [Publish]
    public class HelloWorldPub : IPublisher<HelloWorld>
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

        public Task PublishAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }
    }
}