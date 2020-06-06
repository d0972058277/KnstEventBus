using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnstAsyncApi.Middlewares;
using KnstEventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toys.Models;
using Toys.Pubs;
using Toys.Subs;

namespace Toys
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPublisher<HelloWorld>, HelloWorldPub>();
            services.AddScoped<ISubscriber<HelloWorld>, HelloWorldSub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());

            app.UseMiddleware<AsyncApiMiddleware>();
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