using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus.Channels;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/LightingMeasured")]
    public class LightingMeasuredChannel : IPubChannel<LightMeasured>
    {
        /// <summary>
        /// LightingMeasured Publish Summary
        /// </summary>
        [Publish]
        [Message(typeof(LightMeasured))]
        public Task PublishAsync(LightMeasured @event)
        {
            throw new System.NotImplementedException();
        }
    }
}