using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<Type, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public InMemoryEventBusSubscriptionsManager()
        {
            _eventTypes = new List<Type>();
            _handlers = new Dictionary<Type, List<SubscriptionInfo>>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public event EventHandler<Type> OnEventRemoved;

        public void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            DoAddSubscription(typeof(T), typeof(TH));

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        private void DoAddSubscription(Type eventType, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventType))
            {
                _handlers.Add(eventType, new List<SubscriptionInfo>());
            }

            if (_handlers[eventType].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventType.Name}'", nameof(handlerType));
            }

            _handlers[eventType].Add(SubscriptionInfo.Typed(handlerType));
        }

        public void Clear() => _handlers.Clear();

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent => HasSubscriptionsForEvent(typeof(T));

        public bool HasSubscriptionsForEvent(Type eventType) => _handlers.ContainsKey(eventType);

        public void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
        }

        private void DoRemoveHandler(Type eventType, SubscriptionInfo subsToRemove)
        {
            if (subsToRemove != null)
            {
                _handlers[eventType].Remove(subsToRemove);
                if (!_handlers[eventType].Any())
                {
                    _handlers.Remove(eventType);
                    _eventTypes.Remove(eventType);
                    RaiseOnEventRemoved(eventType);
                }
            }
        }

        private SubscriptionInfo FindSubscriptionToRemove<T, TH>() => FindSubscriptionToRemove(typeof(T), typeof(TH));

        private SubscriptionInfo FindSubscriptionToRemove(Type eventType, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventType))
            {
                return null;
            }

            return _handlers[eventType].SingleOrDefault(s => s.HandlerType == handlerType);
        }

        private void RaiseOnEventRemoved(Type eventType)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventType);
        }
    }
}