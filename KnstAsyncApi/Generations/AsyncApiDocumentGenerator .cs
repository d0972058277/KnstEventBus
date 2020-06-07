using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.Attributes.Marks;
using KnstAsyncApi.Schemas;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;

namespace KnstAsyncApi.Generations
{
    public class AsyncApiDocumentGenerator : IAsyncApiDocumentGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;

        public AsyncApiDocumentGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options)
        {
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
        }

        public AsyncApiDocument GetDocument()
        {
            var result = (AsyncApiDocumentV2) _options.AsyncApi;
            result.Channels = GenerateChannels();
            return result;
        }

        private Channels GenerateChannels()
        {
            var channels = new Channels();

            var channelsMarksAssemblies = GetChannelsMarksAssemblies();
            foreach (var cma in channelsMarksAssemblies)
            {
                var ca = (ChannelAttribute) cma.GetCustomAttributes(typeof(ChannelAttribute), true).SingleOrDefault();
                var pa = (PublishAttribute) cma.GetCustomAttributes(typeof(PublishAttribute), true).SingleOrDefault();
                var sa = (SubscribeAttribute) cma.GetCustomAttributes(typeof(SubscribeAttribute), true).SingleOrDefault();

                var channelItem = new ChannelItem
                {
                    Description = ca.Description,
                    // Parameters = mc.Channel.Parameters,
                };
            }

            return channels;
        }

        private Operation GenerateOperation(TypeInfo cma, OperationAttribute operationAttribute)
        {
            var operation = new Operation
            {
                OperationId = operationAttribute.OperationId ?? cma.FullName + $".{operationAttribute.Type}"
            };

            return operation;
        }

        private TypeInfo[] GetChannelsMarksAssemblies()
        {
            var channelsMarksAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.DefinedTypes.Where(t => t.GetCustomAttributes(typeof(IChannelsMark), true).Length > 0))
                .ToArray();
            return channelsMarksAssemblies;
        }
    }
}