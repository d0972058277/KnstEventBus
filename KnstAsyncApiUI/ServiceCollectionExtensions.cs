using System;
using System.Diagnostics;
using Jering.Javascript.NodeJS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncApiUI(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddNodeJS();

            try
            {
                var info = new ProcessStartInfo("npm")
                {
                    Arguments = $"install",
                    WorkingDirectory = AppContext.BaseDirectory
                };
                var npmInstall = Process.Start(info);
                npmInstall.WaitForExit();
            }
            catch (Exception ex)
            {
                throw new Exception("Node.js/npm is required to build this project. To continue, please install Node.js from https://nodejs.org/ or Visual Studio Installer, and then restart your command prompt or IDE.", ex);
            }

            return services;
        }
    }
}