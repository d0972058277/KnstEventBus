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
            var asyncApiDocumentMarks = GetAsyncApiDocumentMarks();

            var channels = new Channels();
            foreach (var asyncApiDocumentMark in asyncApiDocumentMarks)
            {
                var channelAttribute = (ChannelAttribute) asyncApiDocumentMark.GetCustomAttributes(typeof(ChannelAttribute), true).SingleOrDefault();
                var channelField = new ChannelsFieldName(channelAttribute.Uri);
                var channelItem = new ChannelItem();
                if (channels.ContainsKey(channelField))
                {
                    channelItem = channels[channelField];
                }
                else
                {
                    channels.Add(channelField, channelItem);
                }

                var pub = (PublishAttribute) asyncApiDocumentMark.GetCustomAttributes(typeof(PublishAttribute), true).SingleOrDefault();
                if (pub != null)
                {
                    var operation = new Operation()
                    {
                    OperationId = asyncApiDocumentMark.Name
                    };
                    channelItem.Publish = operation;
                }
                var sub = (SubscribeAttribute) asyncApiDocumentMark.GetCustomAttributes(typeof(SubscribeAttribute), true).SingleOrDefault();
                if (sub != null)
                {
                    var operation = new Operation()
                    {
                    OperationId = asyncApiDocumentMark.Name
                    };
                    channelItem.Subscribe = operation;
                }

            }

            var result = (AsyncApiDocumentV2) _options.AsyncApi;
            result.Channels = channels;
            return result;
        }

        private TypeInfo[] GetAsyncApiDocumentMarks()
        {
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.DefinedTypes.Where(t => t.GetCustomAttributes(typeof(IChannelsMark), true).Length > 0))
                .ToArray();
            return result;
        }
    }
}