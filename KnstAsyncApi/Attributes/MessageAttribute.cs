using System;

namespace KnstAsyncApi.Attributes
{
    public class MessageAttribute : Attribute
    {
        public MessageAttribute(Type payloadType)
        {
            PayloadType = payloadType;
            Title = PayloadType.Name;
            Name = PayloadType.Name;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public Type PayloadType { get; set; }
    }
}