using System;

namespace KnstEventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }

    }
}