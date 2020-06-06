using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubscribeAttribute : OperationAttribute, IChannelsMark
    {
        public SubscribeAttribute(Type messagePayloadType) : base(messagePayloadType)
        {
        }

        public override OperationType? @Type => OperationType.Subscribe;
    }
}