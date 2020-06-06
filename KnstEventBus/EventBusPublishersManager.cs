using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public class EventBusPublishersManager : EventBusHandlersManager, IEventBusPublishersManager
    {
        public new bool IsEmpty => base.IsEmpty;

        public void AddPublisher<T>(IPublisher<T> publisher) where T : IntegrationEvent => base.AddHandler<T>(publisher);

        public new void Clear() => base.Clear();

        public IPublisher GetPublisherForEvent<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>
            => (IPublisher) base.GetHandlerForEvent<T, TP>();
        public IEnumerable<IPublisher> GetPublishersForEvent<T>() where T : IntegrationEvent => (IEnumerable<IPublisher>) base.GetHandlersForEvent<T>();

        public bool HasPublisherForEvent(Type eventType) => base.HasHandlerForEvent(eventType);

        public bool HasPublisherForEvent<T>() where T : IntegrationEvent => HasPublisherForEvent(typeof(T));

        public void RemovePublisher<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>
            => base.RemoveHandler<T, TP>();
    }
}