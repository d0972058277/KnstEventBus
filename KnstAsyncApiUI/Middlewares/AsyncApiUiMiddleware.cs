using System;
using System.Net.Http;
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
        private IHttpClientFactory _httpClientFactory;

        public AsyncApiUiMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Invoke(HttpContext context, INodeJSService nodeService)
        {
            if (IsRequestingAsyncApiIndex(context.Request))
            {
                using(var client = _httpClientFactory.CreateClient())
                using(var response = await client.GetAsync($"{context.Request.Scheme}://{context.Request.Host.Value}/asyncapi/asyncapi.json"))
                {
                    var asyncapiDocument = await response.Content.ReadAsStringAsync();
                    await nodeService.InvokeFromFileAsync($"{AppDomain.CurrentDomain.BaseDirectory}/app.js", args : new [] { asyncapiDocument, "AsyncApi" });
                }
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