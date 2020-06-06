using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;

        Task AddPublisherAsync<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>;

        Task RemovePublisherAsync<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>;

        Task SubscribeAsync<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>;

        Task UnsubscribeAsync<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>;
    }
}