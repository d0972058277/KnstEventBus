using System;
using System.Diagnostics;
using Jering.Javascript.NodeJS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApiUI(this IServiceCollection services)
        {
            services.AddNodeJS();

            var info = new ProcessStartInfo("npm")
            {
                Arguments = $"install",
                WorkingDirectory = AppContext.BaseDirectory
            };
            var npmInstall = Process.Start(info);
            npmInstall.WaitForExit();

            return services;
        }
    }
}