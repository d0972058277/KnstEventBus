using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels.HelloWorlds
{
    [Channel("toys/helloworld")]
    [Publish(typeof(HelloWorld))]
    [Subscribe(typeof(HelloWorld))]
    public class HelloWorldChannel : ChannelBase<HelloWorld>
    {
        public HelloWorldChannel(HelloWorldPub publisher, HelloWorldSub subscriber) : base(publisher, subscriber) { }

        public override Task CloseChannelAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task OpenChannelAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}