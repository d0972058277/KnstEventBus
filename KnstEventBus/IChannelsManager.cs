using System;

namespace KnstEventBus
{
    public interface IChannelsManager
    {
        bool IsEmpty { get; }

        void Clear();

        void AddChannel<T>(IChannel<T> subscriber)
        where T : IntegrationEvent;

        void RemoveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;

        bool HasPublisherForEvent(Type eventType);

        bool HasPublisherForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriberForEvent(Type eventType);

        bool HasSubscriberForEvent<T>() where T : IntegrationEvent;

        IChannel<T> GetChannelForEvent<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;

        IPublisher<T> GetPublisherForEvent<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>;

        ISubscriber<T> GetSubscriberForEvent<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>;
    }
}