using System;
using System.Net;
using System.Threading.Tasks;
using KnstAsyncApi.Generations;
using KnstAsyncApi.Schemas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnstAsyncApi.Middlewares
{
    public class AsyncApiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AsyncApiOptions> _asyncApiOptionsAccessor;

        public AsyncApiMiddleware(RequestDelegate next, IOptions<AsyncApiOptions> asyncApiOptionsAccessor)
        {
            _next = next;
            _asyncApiOptionsAccessor = asyncApiOptionsAccessor;
        }

        public async Task Invoke(HttpContext context, IAsyncApiDocumentGenerator documentGenerator)
        {
            if (!IsRequestingAsyncApiSchema(context.Request))
            {
                await _next(context);
                return;
            }

            var asyncApiDocument = documentGenerator.GetDocument();
            await RespondWithAsyncApiSchemaJson(context.Response, asyncApiDocument);
        }

        private async Task RespondWithAsyncApiSchemaJson(HttpResponse response, AsyncApiDocument asyncApiDocument)
        {
            var asyncApiDocumentJson = JsonConvert.SerializeObject(
                asyncApiDocument,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            response.StatusCode = (int) HttpStatusCode.OK;
            response.ContentType = "application/json";
            await response.WriteAsync(asyncApiDocumentJson);
        }
        private bool IsRequestingAsyncApiSchema(HttpRequest request)
        {
            return HttpMethods.IsGet(request.Method) &&
                string.Equals(request.Path, _asyncApiOptionsAccessor.Value.Route, StringComparison.OrdinalIgnoreCase);
        }
    }
}