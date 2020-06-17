using System.ComponentModel;
using KnstEventBus;

namespace Toys.Models
{
    public class HelloWorld : IntegrationEvent
    {
        [DefaultValue(new [] { "Hi Toys!", "Hello World!" })]
        public string[] Messages { get; set; }

        [DefaultValue(default(InnerHelloWorld))]
        public InnerHelloWorld InnerHelloWorld { get; set; }
    }

    public class InnerHelloWorld
    {
        [DefaultValue("Good Night!")]
        public string InnerMessage { get; set; }

        [DefaultValue(nameof(HelloWorldEnum.Night))]
        public HelloWorldEnum HelloWorldEnum { get; set; }
    }

    public enum HelloWorldEnum
    {
        Morning,
        Afternoon,
        Evening,
        Night

    }
}