using System.Threading.Tasks;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels.HelloWorlds
{
    public class HelloWorldSub : SubscriberBase<HelloWorld>
    {
        public override Task HandleAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }

        public override Task SubscribeAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task UnsubscribeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}