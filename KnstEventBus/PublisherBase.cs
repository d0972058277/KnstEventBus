using System;
using System.Threading.Tasks;

namespace KnstEventBus
{
    public abstract class PublisherBase<T> : IPublisher<T> where T : IntegrationEvent
    {
        protected IChannel<T> _channel;

        public virtual Task HandleAsync(T @event) => PublishAsync(@event);

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

            if (channel.HasPublisher())
            {
                if (channel.IsSame(this))
                {
                    _channel = channel;
                    return;
                }

                throw new ArgumentException("publisher has set channel.");
            }

            _channel = channel;
            channel.SetPublisher(this);
        }

        public void LeaveChannel()
        {
            if (!HasChannel())
                return;

            var channelTemp = _channel;
            _channel = null;
            channelTemp.RemovePublisher();
        }

        public abstract Task PublishAsync(T @event);
    }
}