using System;
using System.Text.Json;
using KnstAsyncApi.DocumrntGenerations;
using KnstAsyncApi.SchemaGenerators;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApi(this IServiceCollection services, Action<AsyncApiDocumentGeneratorOptions> setupAction)
        {
            services.AddOptions();

            services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();
            services.TryAddTransient<ISchemaGenerator, SchemaGenerator>();
            services.TryAddTransient<IMessageGenerator, MessageGenerator>();
            services.TryAddTransient<IDataContractResolver, NewtonsoftDataContractResolver>();
            services.TryAddTransient<JsonSerializerSettings>();

            if (setupAction != null) services.Configure(setupAction);

            return services;
        }
    }
}