using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KnstEventBus
{
    public class EventBusBase : IEventBus
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IEventBusPublishersManager _publishersManager;
        protected readonly IEventBusSubscribersManager _subscribersManager;

        public EventBusBase(IServiceProvider serviceProvider, IEventBusPublishersManager publishersManager, IEventBusSubscribersManager subscribersManager)
        {
            _publishersManager = publishersManager;
            _subscribersManager = subscribersManager;
            _serviceProvider = serviceProvider;
        }

        public async Task AddPublisherAsync<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>
        {
            var publisher = _serviceProvider.GetRequiredService<TP>();
            _publishersManager.AddPublisher<T>(publisher);
            await publisher.OpenChannelAsync();
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            var publishers = (IEnumerable<IPublisher<T>>) _publishersManager.GetPublishersForEvent<T>();
            var tasks = publishers.Select(publisher => publisher.PublishAsync(@event));
            await Task.WhenAll(tasks);
        }

        public async Task RemovePublisherAsync<T, TP>()
        where T : IntegrationEvent
        where TP : IPublisher<T>
        {
            var publisher = (IPublisher<T>) _publishersManager.GetPublisherForEvent<T, TP>();
            _publishersManager.RemovePublisher<T, TP>();
            await publisher.CloseChannelAsync();
        }

        public async Task SubscribeAsync<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>
        {
            var subscriber = _serviceProvider.GetRequiredService<TS>();
            _subscribersManager.AddSubscriber<T>(subscriber);
            await subscriber.OpenChannelAsync();
            await subscriber.SubscribeAsync();
        }

        public async Task UnsubscribeAsync<T, TS>()
        where T : IntegrationEvent
        where TS : ISubscriber<T>
        {
            var subscriber = (ISubscriber<T>) _subscribersManager.GetSubscriberForEvent<T, TS>();
            _subscribersManager.RemoveSubscriber<T, TS>();
            await subscriber.UnsubscribeAsync();
            await subscriber.CloseChannelAsync();
        }
    }
}