using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public class EventBusSubscribersManager : EventBusHandlersManager, IEventBusSubscribersManager
    {
        public new bool IsEmpty => base.IsEmpty;

        public void AddSubscriber<T>(ISubscriber<T> subscriber) where T : IntegrationEvent => base.AddHandler<T>(subscriber);

        public new void Clear() => base.Clear();

        public ISubscriber GetSubscriberForEvent<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>
            => (ISubscriber) base.GetHandlerForEvent<T, TS>();

        public IEnumerable<ISubscriber> GetSubscribersForEvent<T>() where T : IntegrationEvent => (IEnumerable<ISubscriber>) base.GetHandlersForEvent<T>();

        public bool HasSubscriberForEvent(Type eventType) => base.HasHandlerForEvent(eventType);
        public bool HasSubscriberForEvent<T>() where T : IntegrationEvent => HasSubscriberForEvent(typeof(T));

        public void RemoveSubscriber<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>
            => base.RemoveHandler<T, TS>();
    }
}