using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KnstAsyncApiUI.Middlewares
{
    public class AsyncApiUiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILoggerFactory _loggerFactory;

        public AsyncApiUiMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            _next = next;
            _hostingEnv = hostingEnv;
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context, INodeServices nodeServices)
        {
            if (IsRequestingAsyncApiIndex(context.Request))
            {
                var nodeService = context.RequestServices.GetRequiredService<INodeServices>();
                var generateResult = await nodeService.InvokeAsync<string>($"{AppDomain.CurrentDomain.BaseDirectory}/app.js", "https://raw.githubusercontent.com/asyncapi/asyncapi/2.0.0/examples/2.0.0/streetlights.yml");
            }
            await _next(context);
        }

        private bool IsRequestingAsyncApiIndex(HttpRequest request)
        {
            return HttpMethods.IsGet(request.Method) &&
                string.Equals(request.Path, "/index.html", StringComparison.OrdinalIgnoreCase);
        }
    }
}