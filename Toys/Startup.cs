using System;
using System.IO;
using System.Text.Json;
using KnstAsyncApi.DocumrntGenerations;
using KnstAsyncApi.Middlewares;
using KnstAsyncApi.SchemaGenerations;
using KnstAsyncApi.SchemaGenerators;
using KnstAsyncApi.Schemas.V2;
using KnstAsyncApiUI.Middlewares;
using KnstEventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Toys.Channels.HelloWorlds;
using Toys.Models;

namespace Toys
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAsyncApiSchemaGeneration(options =>
            {
                options.AsyncApi = new AsyncApiDocument
                {
                Info = new Info("Toys API", "1.0.0")
                {
                Description = "Knst Toys.",
                },
                // Servers = { { "mosquitto", new Server("test.mosquitto.org", "mqtt") }
                // }
                };
            });
            services.AddOptions();
            services.TryAddTransient<ISchemaGenerator, SchemaGenerator>();
            services.TryAddTransient<IAsyncApiDocumentGenerator, AsyncApiDocumentGenerator>();
            services.TryAddTransient<IDataContractResolver>(sp => new JsonSerializerDataContractResolver(new JsonSerializerOptions()));
            services.AddSingleton<IChannel<HelloWorld>, HelloWorldChannel>();
            services.AddNodeServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseMiddleware<AsyncApiMiddleware>();

            app.UseMiddleware<AsyncApiUiMiddleware>();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot"))
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("This is KnstAsyncApi's toys!");
                });
            });
        }
    }
}