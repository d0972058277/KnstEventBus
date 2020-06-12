using System;
using System.IO;
using KnstAsyncApi.Middlewares;
using KnstAsyncApiUI.Middlewares;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAsyncApi(this IApplicationBuilder app)
        {
            app.UseMiddleware<AsyncApiMiddleware>();
            app.UseAsyncApiUI();

            return app;
        }
    }
}