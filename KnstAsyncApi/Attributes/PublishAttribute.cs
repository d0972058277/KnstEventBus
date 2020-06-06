using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PublishAttribute : OperationAttribute, IChannelsMark
    {
        public PublishAttribute(Type messagePayloadType) : base(messagePayloadType)
        {
        }

        public override OperationType? @Type => OperationType.Publish;
    }
}