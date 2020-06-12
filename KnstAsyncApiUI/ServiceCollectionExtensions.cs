using Jering.Javascript.NodeJS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApiUI(this IServiceCollection services)
        {
            services.AddNodeJS();

            return services;
        }
    }
}
