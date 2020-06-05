using RabbitMQ.Client;

namespace KnstEventBus.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly EventBusRabbitMQConfig _eventBusConfig;
        private readonly IEventBusSubscriptionsManager _subsManager;

        public EventBusRabbitMQ(IConnectionFactory connectionFactory, EventBusRabbitMQConfig eventBusConfig, IEventBusSubscriptionsManager subsManager)
        {
            _connectionFactory = connectionFactory;
            _eventBusConfig = eventBusConfig;
            _subsManager = subsManager;
        }

        public void Publish(IntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }
    }
}