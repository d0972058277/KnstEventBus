using System;
using System.IO;
using KnstAsyncApiUI.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAsyncApiUI(this IApplicationBuilder app)
        {
            app.UseMiddleware<AsyncApiUiMiddleware>();

            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncApi"));
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/asyncapi",
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncApi"))
            });

            return app;
        }
    }
}