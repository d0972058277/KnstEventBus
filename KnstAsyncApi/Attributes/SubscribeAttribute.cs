using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    public class SubscribeAttribute : OperationAttribute, IChannelsMark
    {
        public override OperationType? @Type => OperationType.Subscribe;
    }
}