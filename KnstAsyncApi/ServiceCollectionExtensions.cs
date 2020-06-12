using System;
using System.Text.Json;
using KnstAsyncApi.DocumrntGenerations;
using KnstAsyncApi.SchemaGenerators;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApi(this IServiceCollection services, Action<AsyncApiDocumentGeneratorOptions> setupAction)
        {
            services.AddAsyncApiUI();

            services.AddOptions();

            services.TryAddTransient<IDataContractResolver>(sp => new JsonSerializerDataContractResolver(new JsonSerializerOptions()));
            services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();
            services.TryAddTransient<ISchemaGenerator, SchemaGenerator>();
            services.TryAddTransient<IMessageGenerator, MessageGenerator>();

            if (setupAction != null) services.Configure(setupAction);

            return services;
        }
    }
}