using System.Threading.Tasks;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels.HelloWorlds
{
    public class HelloWorldPub : PublisherBase<HelloWorld>
    {
        public override Task PublishAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }
    }
}