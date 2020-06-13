using System;

namespace KnstAsyncApi.Attributes
{
    public class MessageAttribute : Attribute
    {
        public MessageAttribute(Type payloadType)
        {
            PayloadType = payloadType;
            Title = PayloadType.FullName;
            Name = PayloadType.Name;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public Type PayloadType { get; set; }
    }
}