using System;
using System.Collections.Generic;

namespace KnstEventBus
{
    public interface IChannelsManager
    {
        bool IsEmpty { get; }

        void Clear();

        void AddChannel<T>(IChannel<T> channel)
        where T : IntegrationEvent;

        void RemoveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;

        bool HasChannelForEvent(Type eventType);

        bool HasChannelForEvent<T>() where T : IntegrationEvent;

        IEnumerable<IChannel<T>> GetChannelsForEvent<T>()
        where T : IntegrationEvent;

        IChannel<T> GetChannelForEvent<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>;
    }
}