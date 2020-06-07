using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KnstEventBus
{
    public class EventBusBase : IEventBus
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IChannelsManager _channelsManager;

        public EventBusBase(IServiceProvider serviceProvider, IChannelsManager channelsManager)
        {
            _serviceProvider = serviceProvider;
            _channelsManager = channelsManager;
        }

        public async Task JoinChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
        {
            var channel = _serviceProvider.GetRequiredService<TC>();
            _channelsManager.AddChannel<T>(channel);
            await channel.SubscribeAsync();
        }

        public async Task LeaveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
        {
            var channel = _channelsManager.GetChannelForEvent<T, TC>();
            _channelsManager.RemoveChannel<T, TC>();
            await channel.UnSubscribeAsync();
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            var channels = _channelsManager.GetChannelsForEvent<T>();
            var tasks = channels.Select(c => c.PublishAsync(@event));
            await Task.WhenAll(tasks);
        }
    }
}