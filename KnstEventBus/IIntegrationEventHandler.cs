using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
        Task OpenChannelAsync();
        Task CloseChannelAsync();
    }
}