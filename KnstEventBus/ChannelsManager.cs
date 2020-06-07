using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public class ChannelsManager : IChannelsManager
    {
        private readonly Dictionary<Type, IChannel> _channels;

        public bool IsEmpty => !_channels.Any();

        public void AddChannel<T>(IChannel<T> subscriber) where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public void Clear() => _channels.Clear();

        public IChannel<T> GetChannelForEvent<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
        {
            throw new NotImplementedException();
        }

        public IPublisher<T> GetPublisherForEvent<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>
        {
            throw new NotImplementedException();
        }

        public ISubscriber<T> GetSubscriberForEvent<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>
        {
            throw new NotImplementedException();
        }

        public bool HasPublisherForEvent(Type eventType)
        {
            throw new NotImplementedException();
        }

        public bool HasPublisherForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriberForEvent(Type eventType)
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriberForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public void RemoveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
        {
            throw new NotImplementedException();
        }
    }
}