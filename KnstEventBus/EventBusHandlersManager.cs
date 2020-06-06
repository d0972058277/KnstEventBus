using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public abstract class EventBusHandlersManager
    {
        private readonly Dictionary<Type, Dictionary<Type, IIntegrationEventHandler>> _handlers;

        public EventBusHandlersManager()
        {
            _handlers = new Dictionary<Type, Dictionary<Type, IIntegrationEventHandler>>();
        }

        protected bool IsEmpty => !_handlers.Any();
        protected void AddHandler<T>(IIntegrationEventHandler<T> handler)
        where T : IntegrationEvent
        {
            if (!HasHandlerForEvent<T>())
            {
                _handlers.Add(typeof(T), new Dictionary<Type, IIntegrationEventHandler>());
            }

            if (_handlers[typeof(T)].Any(h => h.GetType() == handler.GetType()))
            {
                throw new ArgumentException($"Handler Type {handler.GetType().Name} already registered for '{typeof(T).Name}'", nameof(handler));
            }

            _handlers[typeof(T)].Add(handler.GetType(), handler);
        }

        protected void RemoveHandler<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            if (_handlers[typeof(T)].ContainsKey(typeof(TH)))
            {
                _handlers[typeof(T)].Remove(typeof(TH));
                if (!_handlers[typeof(T)].Any())
                {
                    _handlers.Remove(typeof(T));
                }
            }
        }

        protected bool HasHandlerForEvent(Type eventType) => _handlers.ContainsKey(eventType);
        protected bool HasHandlerForEvent<T>() where T : IntegrationEvent => HasHandlerForEvent(typeof(T));
        protected void Clear() => _handlers.Clear();
        protected IEnumerable<IIntegrationEventHandler> GetHandlersForEvent<T>() where T : IntegrationEvent => _handlers[typeof(T)].Select(handler => handler.Value);
        protected IIntegrationEventHandler GetHandlerForEvent<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
            => _handlers[typeof(T)][typeof(TH)];
    }
}