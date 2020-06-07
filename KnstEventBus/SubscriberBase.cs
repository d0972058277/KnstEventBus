using System;
using System.Threading.Tasks;

namespace KnstEventBus
{
    public abstract class SubscriberBase<T> : ISubscriber<T> where T : IntegrationEvent
    {
        protected IChannel<T> _channel;

        public abstract Task HandleAsync(T @event);

        public bool HasChannel() => _channel != null;

        public bool IsSame(IChannel<T> channel) => _channel == channel;

        public void JoinChannel(IChannel<T> channel)
        {
            if (HasChannel())
            {
                if (IsSame(channel))
                    return;

                throw new ArgumentException("_channel has set.");
            }

            if (channel.HasSubscriber())
            {
                if (channel.IsSame(this))
                {
                    _channel = channel;
                    return;
                }

                throw new ArgumentException("publisher has set channel.");
            }

            _channel = channel;
            channel.SetSubscriber(this);
        }

        public void LeaveChannel()
        {
            if (!HasChannel())
                return;

            var channelTemp = _channel;
            _channel = null;
            channelTemp.RemoveSubscriber();
        }

        public abstract Task SubscribeAsync();

        public abstract Task UnsubscribeAsync();
    }
}