using System;
using KnstAsyncApi.Generations;
using KnstAsyncApi.Generations.SchemaGeneration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApiSchemaGeneration(this IServiceCollection services, Action<AsyncApiDocumentGeneratorOptions> setupAction)
        {
            services.AddOptions();
            
            services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();
            services.TryAddTransient<ISchemaGenerator, SchemaGenerator>();
            
            if (setupAction != null) services.Configure(setupAction);
            
            return services;
        }
    }
}