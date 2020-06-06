using System;
using System.Collections.Generic;

namespace KnstEventBus
{
    public interface IEventBusPublishersManager
    {
        bool IsEmpty { get; }
        void AddPublisher<T>(IPublisher<T> publisher)
        where T : IntegrationEvent;

        void RemovePublisher<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>;

        bool HasPublisherForEvent(Type eventType);
        bool HasPublisherForEvent<T>() where T : IntegrationEvent;
        void Clear();
        IEnumerable<IPublisher> GetPublishersForEvent<T>() where T : IntegrationEvent;
        IPublisher GetPublisherForEvent<T, TH>()
        where T : IntegrationEvent
        where TH : IPublisher<T>;
    }
}