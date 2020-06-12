using KnstAsyncApi.Middlewares;

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