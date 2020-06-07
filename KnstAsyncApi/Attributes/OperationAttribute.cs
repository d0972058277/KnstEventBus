using System;

namespace KnstAsyncApi.Attributes
{
    public class OperationAttribute : Attribute
    {
        public string OperationId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public virtual OperationType? @Type { get; }
    }

    public enum OperationType
    {
        Publish,
        Subscribe
    }
}