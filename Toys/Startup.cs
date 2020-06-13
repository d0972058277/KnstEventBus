using KnstAsyncApi.Schemas.V2;
using KnstEventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Toys.Channels;
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
                Description = @"|
The Smartylighting Streetlights API allows you to remotely manage the city lights.

### Check out its awesome features:

* Turn a specific streetlight on/off 🌃
* Dim a specific streetlight 😎
* Receive real-time information about environmental lighting conditions 📈"
                }
                // Servers = { { "mosquitto", new Server("test.mosquitto.org", "mqtt") }
                // }
                };
            });
            services.AddAsyncApiUI();

            services.AddTransient<IChannel<LightMeasured>, LightingMeasuredChannel>();
            services.AddTransient<IChannel<TurnOnOff>, StreetlightTurnOnOffChannel>();
            services.AddTransient<IChannel<DimLight>, StreetlightDimChannel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAsyncApi();
            app.UseAsyncApiUI();

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