using KnstAsyncApi.Schemas;

namespace KnstAsyncApi.DocumentGenerations
{
    public interface IAsyncApiDocumentGenerator
    {
        AsyncApiDocument GetDocument();
    }
}