using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/StreetlightDim")]
    [Message(typeof(DimLight))]
    public class StreetlightDimChannel : IChannel<DimLight>
    {
        public Task PublishAsync(DimLight @event) => Task.CompletedTask;

        /// <summary>
        /// StreetlightDim Subscribe Summary
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