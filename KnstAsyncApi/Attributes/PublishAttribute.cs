namespace KnstAsyncApi.Attributes
{
    public class PublishAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Publish;
    }
}