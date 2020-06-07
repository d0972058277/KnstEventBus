using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface ISubscriber<T> : ISubscriber, IIntegrationEventHandler<T> where T : IntegrationEvent
    {
        bool HasChannel();
        void JoinChannel(IChannel<T> channel);
        void LeaveChannel();
        bool IsSame(IChannel<T> channel);
        Task SubscribeAsync();
        Task UnsubscribeAsync();
    }

    public interface ISubscriber : IIntegrationEventHandler { }
}