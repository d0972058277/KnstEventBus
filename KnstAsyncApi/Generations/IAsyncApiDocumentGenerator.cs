using KnstAsyncApi.Schemas;

namespace KnstAsyncApi.Generations
{
    public interface IAsyncApiDocumentGenerator
    {
        AsyncApiDocument GetDocument();
    }
}