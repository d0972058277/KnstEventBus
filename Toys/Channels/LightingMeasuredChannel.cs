using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/LightingMeasured")]
    [Message(typeof(LightMeasured))]
    public class LightingMeasuredChannel : IChannel<LightMeasured>
    {
        /// <summary>
        /// LightingMeasured Publish Summary
        /// </summary>
        [Publish]
        public Task PublishAsync(LightMeasured @event)
        {
            throw new System.NotImplementedException();
        }

        public Task SubscribeAsync() => Task.CompletedTask;

        public Task UnSubscribeAsync() => Task.CompletedTask;
    }
}