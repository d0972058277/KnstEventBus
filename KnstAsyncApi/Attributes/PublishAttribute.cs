using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    public class PublishAttribute : OperationAttribute, IChannelsMark
    {
        public override OperationType? @Type => OperationType.Publish;
    }
}