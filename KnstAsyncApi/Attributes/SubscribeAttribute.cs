using System;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SubscribeAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Subscribe;
    }
}