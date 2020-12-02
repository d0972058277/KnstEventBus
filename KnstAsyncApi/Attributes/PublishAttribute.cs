using System;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PublishAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Publish;
    }
}