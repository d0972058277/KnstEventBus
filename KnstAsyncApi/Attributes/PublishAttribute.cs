using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    public class PublishAttribute : OperationAttribute, IChannelsMark
    {
        public PublishAttribute(Type messageType) : base(messageType) { }

        public override OperationType? @Type => OperationType.Publish;
    }
}