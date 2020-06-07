using System;
using System.Threading.Tasks;

namespace KnstEventBus
{
    public abstract class ChannelBase<T> : IChannel<T> where T : IntegrationEvent
    {
        protected IPublisher<T> _publisher;
        protected ISubscriber<T> _subscriber;

        protected ChannelBase() { }

        protected ChannelBase(IPublisher<T> publisher)
        {
            _publisher = publisher;
            publisher.JoinChannel(this);
        }

        protected ChannelBase(ISubscriber<T> subscriber)
        {
            _subscriber = subscriber;
            subscriber.JoinChannel(this);
        }

        protected ChannelBase(IPublisher<T> publisher, ISubscriber<T> subscriber)
        {
            _publisher = publisher;
            _subscriber = subscriber;
            publisher.JoinChannel(this);
            subscriber.JoinChannel(this);
        }

        public abstract Task CloseChannelAsync();

        public IPublisher<T> GetPublisher() => _publisher;

        public ISubscriber<T> GetSubscriber() => _subscriber;

        public bool HasPublisher() => _publisher != null;

        public bool HasSubscriber() => _subscriber != null;

        public bool IsSame(IPublisher<T> publisher) => _publisher == publisher;

        public bool IsSame(ISubscriber<T> subscriber) => _subscriber == subscriber;

        public abstract Task OpenChannelAsync();

        public void RemovePublisher()
        {
            if (!HasPublisher())
                return;

            var publisherTemp = _publisher;
            _publisher = null;
            publisherTemp.LeaveChannel();
        }

        public void RemoveSubscriber()
        {
            if (!HasSubscriber())
                return;

            var subscriberTemp = _subscriber;
            _subscriber = null;
            subscriberTemp.LeaveChannel();
        }

        public void SetPublisher(IPublisher<T> publisher)
        {
            if (HasPublisher())
            {
                if (IsSame(publisher))
                    return;

                throw new ArgumentException("_publisher has set.");
            }

            if (publisher.HasChannel())
            {
                if (publisher.IsSame(this))
                {
                    _publisher = publisher;
                    return;
                }

                throw new ArgumentException("publisher has set channel.");
            }

            _publisher = publisher;
            publisher.JoinChannel(this);
        }

        public void SetSubscriber(ISubscriber<T> subscriber)
        {
            if (HasSubscriber())
            {
                if (IsSame(subscriber))
                    return;

                throw new ArgumentException("_publisher has set.");
            }

            if (subscriber.HasChannel())
            {
                if (subscriber.IsSame(this))
                {
                    _subscriber = subscriber;
                    return;
                }

                throw new ArgumentException("publisher has set channel.");
            }

            _subscriber = subscriber;
            subscriber.JoinChannel(this);
        }
    }
}