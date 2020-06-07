using System;
using System.Threading.Tasks;

namespace KnstEventBus
{
    public interface IChannel<T> : IChannel where T : IntegrationEvent
    {
        bool HasPublisher();
        bool HasSubscriber();
        void SetPublisher(IPublisher<T> publisher);
        void SetSubscriber(ISubscriber<T> subscriber);
        IPublisher<T> GetPublisher();
        ISubscriber<T> GetSubscriber();
        void RemoveSubscriber();
        void RemovePublisher();
        bool IsSame(IPublisher<T> publisher);
        bool IsSame(ISubscriber<T> subscriber);
    }

    public interface IChannel
    {
        Task OpenChannelAsync();
        Task CloseChannelAsync();
    }
}