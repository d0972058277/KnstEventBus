using System.Threading.Tasks;
using KnstAsyncApi.Attributes;
using KnstEventBus;
using Toys.Models;

namespace Toys.Channels
{
    [AsyncApi]
    [Channel("pubsub/StreetlightTurnOnOff")]
    [Message(typeof(TurnOnOff))]
    public class StreetlightTurnOnOffChannel : IChannel<TurnOnOff>
    {
        public Task PublishAsync(TurnOnOff @event) => Task.CompletedTask;

        /// <summary>
        /// StreetlightTurnOnOff Subscribe Summary
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