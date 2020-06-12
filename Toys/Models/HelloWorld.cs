using KnstEventBus;

namespace Toys.Models
{
    /// <summary>
    /// HelloWorld Summery
    /// </summary>
    public class HelloWorld : IntegrationEvent
    {
        public string[] Messages { get; set; }
        public InnerHelloWorld InnerHelloWorld { get; set; }
    }

    public class InnerHelloWorld
    {
        public string InnerMessage { get; set; }
        public HelloWorldEnum HelloWorldEnum { get; set; }
    }

    public enum HelloWorldEnum
    {
        Test1,
        Test2,
        Test3
    }
}