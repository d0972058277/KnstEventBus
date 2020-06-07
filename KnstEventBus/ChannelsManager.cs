using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstEventBus
{
    public class ChannelsManager : IChannelsManager
    {
        private readonly Dictionary<Type, Dictionary<Type, IChannel>> _channels;

        public bool IsEmpty => !_channels.Any();

        public void AddChannel<T>(IChannel<T> channel) where T : IntegrationEvent
        {
            if (!HasChannelForEvent<T>())
            {
                _channels.Add(typeof(T), new Dictionary<Type, IChannel>());
            }

            if (_channels[typeof(T)].Any(h => h.GetType() == channel.GetType()))
            {
                throw new ArgumentException($"Channel Type {channel.GetType().Name} already registered for '{typeof(T).Name}'", nameof(channel));
            }

            _channels[typeof(T)].Add(channel.GetType(), channel);
        }

        public void Clear() => _channels.Clear();

        public IChannel<T> GetChannelForEvent<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
            => (IChannel<T>) _channels[typeof(T)][typeof(TC)];

        public IEnumerable<IChannel<T>> GetChannelsForEvent<T>() where T : IntegrationEvent => (IEnumerable<IChannel<T>>) _channels[typeof(T)].Select(c => c.Value);

        public bool HasChannelForEvent(Type eventType) => _channels.ContainsKey(eventType);

        public bool HasChannelForEvent<T>() where T : IntegrationEvent => HasChannelForEvent(typeof(T));

        public void RemoveChannel<T, TC>()
        where T : IntegrationEvent
        where TC : IChannel<T>
        {
            if (_channels[typeof(T)].ContainsKey(typeof(TC)))
            {
                _channels[typeof(T)].Remove(typeof(TC));
                if (!_channels[typeof(T)].Any())
                {
                    _channels.Remove(typeof(T));
                }
            }
        }
    }
}