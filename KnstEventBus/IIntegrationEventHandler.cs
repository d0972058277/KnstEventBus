using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IIntegrationEventHandler<T> : IIntegrationEventHandler
    where T : IntegrationEvent
    {
        Task HandleAsync(T @event);
    }

    public interface IIntegrationEventHandler { }
}