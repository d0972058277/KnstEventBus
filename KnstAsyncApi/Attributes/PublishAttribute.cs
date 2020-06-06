using System;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PublishAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Publish;
    }
}