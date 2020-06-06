using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OperationAttribute : Attribute, IChannelsMark
    {
        public OperationAttribute(Type messagePayloadType)
        {
            MessagePayloadType = messagePayloadType;
        }

        public Type MessagePayloadType { get; }
        public string OperationId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public virtual OperationType? @Type { get; }
    }

    public enum OperationType
    {
        Publish,
        Subscribe
    }
}