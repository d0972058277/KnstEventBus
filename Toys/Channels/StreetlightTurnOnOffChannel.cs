using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using KnstEventBus.Channels;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/StreetlightTurnOnOff")]
    public class StreetlightTurnOnOffChannel : ISubChannel
    {
        public Task PublishAsync(TurnOnOff @event) => Task.CompletedTask;

        /// <summary>
        /// StreetlightTurnOnOff Subscribe Summary
        /// </summary>
        /// <returns></returns>
        [Subscribe]
        [Message(typeof(TurnOnOff))]
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