using System;
using System.Collections.Generic;
using static KnstEventBus.InMemoryEventBusSubscriptionsManager;

namespace KnstEventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<Type> OnEventRemoved;

        void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

        void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent(Type eventType);
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    }
}