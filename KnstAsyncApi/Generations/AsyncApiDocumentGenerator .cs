using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.Attributes.Marks;
using KnstAsyncApi.Schemas;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;
using Namotion.Reflection;

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
            foreach (var channelsMarksAssembly in channelsMarksAssemblies)
            {
                var channelAttribute = (ChannelAttribute) channelsMarksAssembly.GetCustomAttributes(typeof(ChannelAttribute), true).Single();

                var methods = channelsMarksAssembly.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(OperationAttribute), true) != null).ToArray();

                var publishMethod = methods.Where(m => m.GetCustomAttribute(typeof(PublishAttribute), true) != null).SingleOrDefault();
                var subscribeMethod = methods.Where(m => m.GetCustomAttribute(typeof(SubscribeAttribute), true) != null).SingleOrDefault();

                var channelItem = new ChannelItem
                {
                    Description = channelAttribute.Description,
                    // Parameters = mc.Channel.Parameters,
                    Publish = GenerateOperation(channelsMarksAssembly, publishMethod),
                    Subscribe = GenerateOperation(channelsMarksAssembly, subscribeMethod)
                };
            }

            return channels;
        }

        private Operation GenerateOperation(TypeInfo channelsMarksAssembly, MethodInfo method)
        {
            if (method == null) return null;

            var operationAttribute = (OperationAttribute) method.GetCustomAttribute(typeof(OperationAttribute), true);

            var operation = new Operation
            {
                OperationId = operationAttribute.OperationId ?? channelsMarksAssembly.FullName + $".{operationAttribute.Type}",
                Summary = operationAttribute.Summary ?? method.GetXmlDocsSummary(),
                Description = operationAttribute.Description ?? (method.GetXmlDocsRemarks() != "" ? method.GetXmlDocsRemarks() : null),
            };

            return operation;
        }

        private Message GenerateMessage(TypeInfo channelsMarksAssembly)
        {
            var messagePayloadAttribute = (MessagePayloadAttribute) channelsMarksAssembly.GetCustomAttributes(typeof(MessagePayloadAttribute), true).Single();

            var message = new Message { };

            return message;
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