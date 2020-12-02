using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using KnstEventBus.Channels;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/StreetlightDim")]
    public class StreetlightDimChannel : ISubChannel
    {
        public Task PublishAsync(DimLight @event) => Task.CompletedTask;

        /// <summary>
        /// StreetlightDim Subscribe Summary
        /// </summary>
        /// <returns></returns>
        [Subscribe]
        [Message(typeof(DimLight))]
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