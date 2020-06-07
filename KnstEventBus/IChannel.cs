using System;
using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IChannel<T> : IChannel where T : IntegrationEvent
    {
        Task PublishAsync(T @event);
        Task SubscribeAsync();
        Task UnSubscribeAsync();
    }

    public interface IChannel { }
}