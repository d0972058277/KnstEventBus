using System;
using KnstAsyncApi.DocumentSchemas;
using Microsoft.Extensions.Options;

namespace KnstAsyncApi.DocumentGenerations
{
    public class AsyncApiDocumentGenerator : IAsyncApiDocumentGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;

        public AsyncApiDocumentGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options)
        {
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
        }

        public AsyncApiDocument GetDocument()
        {
            throw new System.NotImplementedException();
        }
    }
}