using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;
        
        Task JoinChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;

        Task LeaveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;
    }
}