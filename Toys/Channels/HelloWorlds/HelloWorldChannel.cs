using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels.HelloWorlds
{
    /// <summary>
    /// HelloWorldChannel Summary
    /// </summary>
    [AsyncApi]
    [Channel("pubsub/HelloWorld")]
    [Message(typeof(HelloWorld))]
    public class HelloWorldChannel : IChannel<HelloWorld>
    {
        /// <summary>
        /// HelloWorldChannel PublishAsync Summary
        /// </summary>
        [Publish]
        public Task PublishAsync(HelloWorld @event)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// HelloWorldChannel SubscribeAsync Summary
        /// </summary>
        /// <returns></returns>
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