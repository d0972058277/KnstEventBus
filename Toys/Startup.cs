using KnstAsyncApi.Schemas.V2;
using KnstEventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddAsyncApi(options =>
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
            services.AddSingleton<IChannel<HelloWorld>, HelloWorldChannel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAsyncApi();

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