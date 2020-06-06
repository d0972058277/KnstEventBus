using KnstEventBus;

namespace Toys.Models
{
    public class HelloWorld : IntegrationEvent
    {
        public string Message { get; set; }
    }
}