using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    public class SubscribeAttribute : OperationAttribute, IChannelsMark
    {
        public SubscribeAttribute(Type messageType) : base(messageType) { }

        public override OperationType? @Type => OperationType.Subscribe;
    }
}