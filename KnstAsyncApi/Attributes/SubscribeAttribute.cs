namespace KnstAsyncApi.Attributes
{
    public class SubscribeAttribute : OperationAttribute
    {
        public override OperationType? @Type => OperationType.Subscribe;
    }
}