using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IPublisher<T> : IPublisher, IIntegrationEventHandler<T> where T : IntegrationEvent
    {
        bool HasChannel();
        void JoinChannel(IChannel<T> channel);
        void LeaveChannel();
        bool IsSame(IChannel<T> channel);
        Task PublishAsync(T @event);
    }

    public interface IPublisher : IIntegrationEventHandler { }
}