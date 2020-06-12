using System;
using System.Threading.Tasks;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KnstAsyncApiUI.Middlewares
{
    public class AsyncApiUiMiddleware
    {
        private readonly RequestDelegate _next;

        public AsyncApiUiMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, INodeJSService nodeService)
        {
            if (IsRequestingAsyncApiIndex(context.Request))
            {
                await nodeService.InvokeFromFileAsync($"{AppDomain.CurrentDomain.BaseDirectory}/app.js", args: new[] { $"{context.Request.Scheme}://{context.Request.Host.Value}/asyncapi/asyncapi.json", "AsyncApi" });
            }
            await _next(context);
        }

        private bool IsRequestingAsyncApiIndex(HttpRequest request)
        {
            return HttpMethods.IsGet(request.Method) &&
                string.Equals(request.Path, "/asyncapi/index.html", StringComparison.OrdinalIgnoreCase);
        }
    }
}