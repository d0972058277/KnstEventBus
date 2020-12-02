using System;
using System.Collections.Generic;
using System.Linq;
using KnstEventBus.Bus;
using KnstEventBus.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace KnstEventBus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKnstEventBus(this IServiceCollection services)
        {
            services.AddPubChannels();
            services.AddSubChannels();
            services.AddTransient<IPubBus, PubBus>();
            services.AddTransient<ISubBus, SubBus>();
            return services;
        }

        public static IServiceCollection AddPubChannels(this IServiceCollection services)
        {
            var channelTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IPubChannel<>)) && !p.IsInterface && !p.IsAbstract)
                .ToArray();
            foreach (var channelType in channelTypes)
            {
                var ipubType = channelType.GetInterfaces().Where(t => t.GetGenericTypeDefinition() == typeof(IPubChannel<>)).First();
                services.AddTransient(channelType);
                services.AddTransient(typeof(IPubChannel), channelType);
                services.AddTransient(ipubType, channelType);
            }

            services.AddTransient<Dictionary<Type, List<IPubChannel>>>(sp =>
            {
                var bus = new Dictionary<Type, List<IPubChannel>>();

                var pubs = sp.GetRequiredService<IEnumerable<IPubChannel>>();
                foreach (var pub in pubs)
                {
                    var ipubType = pub.GetType().GetInterfaces().Where(t => t.GetGenericTypeDefinition() == typeof(IPubChannel<>)).First();
                    var argType = ipubType.GetGenericArguments().First();

                    if (!bus.TryGetValue(argType, out List<IPubChannel> pubChannels))
                    {
                        pubChannels = new List<IPubChannel>();
                        bus.Add(argType, pubChannels);
                    }

                    pubChannels.Add(pub);
                }

                return bus;
            });

            return services;
        }

        public static IServiceCollection AddSubChannels(this IServiceCollection services)
        {
            var channelTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetInterfaces().Any(t => t == typeof(ISubChannel)) && !p.IsInterface && !p.IsAbstract)
                .ToArray();
            foreach (var channelType in channelTypes)
            {
                services.AddTransient(channelType);
                services.AddTransient(typeof(ISubChannel), channelType);
            }

            return services;
        }

        public static IServiceCollection AddSubscribeBackgroundService(this IServiceCollection services)
        {
            services.AddHostedService<SubscribeBackgroundService>();
            return services;
        }
    }
}