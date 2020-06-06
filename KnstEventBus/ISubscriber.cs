using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface ISubscriber<in TIntegrationEvent> : ISubscriber, IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task SubscribeAsync();
        Task UnsubscribeAsync();
    }

    public interface ISubscriber : IIntegrationEventHandler { }
}