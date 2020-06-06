using System;
using System.Collections.Generic;

namespace KnstEventBus
{
    public interface IEventBusSubscribersManager
    {
        bool IsEmpty { get; }
        void AddSubscriber<T>(ISubscriber<T> subscriber)
        where T : IntegrationEvent;

        void RemoveSubscriber<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>;

        bool HasSubscriberForEvent(Type eventType);
        bool HasSubscriberForEvent<T>() where T : IntegrationEvent;
        void Clear();
        IEnumerable<ISubscriber> GetSubscribersForEvent<T>() where T : IntegrationEvent;
        ISubscriber GetSubscriberForEvent<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>;
    }
}