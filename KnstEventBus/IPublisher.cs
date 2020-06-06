using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IPublisher<in TIntegrationEvent> : IPublisher, IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task PublishAsync(TIntegrationEvent @event);
    }

    public interface IPublisher : IIntegrationEventHandler { }
}