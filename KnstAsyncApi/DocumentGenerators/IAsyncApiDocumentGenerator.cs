using KnstAsyncApi.Schemas;
using KnstAsyncApi.Schemas.V2;

namespace KnstAsyncApi.DocumrntGenerations
{
    public interface IAsyncApiDocumentGenerator
    {
        AsyncApiDocument GetDocument();
    }
}