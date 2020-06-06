using System;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SubscribeAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Subscribe;
    }
}