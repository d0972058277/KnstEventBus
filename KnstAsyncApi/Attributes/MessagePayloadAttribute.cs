using System;

namespace KnstAsyncApi.Attributes
{
    public class MessagePayloadAttribute : Attribute
    {
        public MessagePayloadAttribute(Type messagePayloadType)
        {
            MessagePayloadType = messagePayloadType ??
                throw new ArgumentNullException(nameof(messagePayloadType));
        }

        public Type MessagePayloadType { get; }
        public string Summary { get; set; }
    }
}