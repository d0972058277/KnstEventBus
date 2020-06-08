using System;
using System.Linq;
using System.Reflection;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.SchemaGenerations;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;
using Namotion.Reflection;

namespace KnstAsyncApi.DocumrntGenerations
{
    public class AsyncApiDocumentGenerator : IAsyncApiDocumentGenerator
    {
        private readonly ISchemaGenerator _schemaGenerator;
        private readonly AsyncApiDocumentGeneratorOptions _options;

        public AsyncApiDocumentGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options, ISchemaGenerator schemaGenerator)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _schemaGenerator = schemaGenerator;
        }

        public AsyncApiDocument GetDocument()
        {
            var schemaRepository = new SchemaRepository();

            var asyncApi = _options.AsyncApi;
            asyncApi.Channels = GenerateChannels(schemaRepository);
            asyncApi.Components.Schemas = schemaRepository.Schemas;

            return asyncApi;
        }

        private Channels GenerateChannels(ISchemaRepository schemaRepository)
        {
            var channels = new Channels();

            var asyncApiTypeInfos = GetAsyncApiTypeInfos();
            foreach (var asyncApiTypeInfo in asyncApiTypeInfos)
            {
                var channelAttribute = (ChannelAttribute)asyncApiTypeInfo.GetCustomAttributes(typeof(ChannelAttribute), true).Single();

                var methods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(OperationAttribute), true) != null).ToArray();

                var publishMethod = methods.Where(m => m.GetCustomAttribute(typeof(PublishAttribute), true) != null).SingleOrDefault();
                var subscribeMethod = methods.Where(m => m.GetCustomAttribute(typeof(SubscribeAttribute), true) != null).SingleOrDefault();

                var channelItem = new ChannelItem
                {
                    Description = channelAttribute.Description,
                    // Parameters = mc.Channel.Parameters,
                    Publish = GenerateOperation(asyncApiTypeInfo, publishMethod, schemaRepository),
                    Subscribe = GenerateOperation(asyncApiTypeInfo, subscribeMethod, schemaRepository)
                };

                channels.Add(channelAttribute.Uri, channelItem);
            }

            return channels;
        }

        private Operation GenerateOperation(TypeInfo asyncApiTypeInfo, MethodInfo method, ISchemaRepository schemaRepository)
        {
            if (method == null) return null;

            var operationAttribute = (OperationAttribute)method.GetCustomAttribute(typeof(OperationAttribute), true);

            var operation = new Operation
            {
                OperationId = operationAttribute.OperationId ?? asyncApiTypeInfo.FullName + $".{operationAttribute.Type}",
                Summary = operationAttribute.Summary ?? method.GetXmlDocsSummary(),
                Description = operationAttribute.Description ?? (method.GetXmlDocsRemarks() != "" ? method.GetXmlDocsRemarks() : null),
                Message = GenerateMessage(asyncApiTypeInfo, schemaRepository)
            };

            return operation;
        }

        private Message GenerateMessage(TypeInfo channelsMarksAssembly, ISchemaRepository schemaRepository)
        {
            var messagePayloadAttribute = (MessagePayloadAttribute)channelsMarksAssembly.GetCustomAttributes(typeof(MessagePayloadAttribute), true).Single();

            var message = new Message
            {
                Payload = _schemaGenerator.GenerateSchema(messagePayloadAttribute.MessagePayloadType, schemaRepository),
                // todo: all the other properties... message has a lot!
            };

            return message;
        }

        private TypeInfo[] GetAsyncApiTypeInfos()
        {
            var channelsMarksAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.DefinedTypes.Where(t => t.GetCustomAttributes(typeof(AsyncApiAttribute), true).Length > 0))
                .ToArray();
            return channelsMarksAssemblies;
        }
    }
}