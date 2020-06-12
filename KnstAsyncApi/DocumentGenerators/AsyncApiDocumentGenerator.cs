using System;
using System.Linq;
using System.Reflection;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.SchemaGenerators;
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
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
            _schemaGenerator = schemaGenerator;
        }

        public AsyncApiDocument GetDocument()
        {
            var schemaRepository = new SchemaRepository();
            var messageRepository = new MessageRepository();

            var asyncApi = _options.AsyncApi;
            asyncApi.Channels = GenerateChannels(schemaRepository, messageRepository);
            asyncApi.Components.Messages = messageRepository.Messages;
            asyncApi.Components.Schemas = schemaRepository.Schemas;

            return asyncApi;
        }

        private Channels GenerateChannels(SchemaRepository schemaRepository, MessageRepository messageRepository)
        {
            var channels = new Channels();

            var asyncApiTypeInfos = GetAsyncApiTypeInfos();
            foreach (var asyncApiTypeInfo in asyncApiTypeInfos)
            {
                var channelAttribute = (ChannelAttribute) asyncApiTypeInfo.GetCustomAttributes(typeof(ChannelAttribute), true).Single();

                var methods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(OperationAttribute), true) != null).ToArray();

                var publishMethod = methods.Where(m => m.GetCustomAttribute(typeof(PublishAttribute), true) != null).SingleOrDefault();
                var subscribeMethod = methods.Where(m => m.GetCustomAttribute(typeof(SubscribeAttribute), true) != null).SingleOrDefault();

                var channelItem = new ChannelItem
                {
                    Description = channelAttribute.Description,
                    // Parameters = mc.Channel.Parameters,
                    Publish = GenerateOperation(asyncApiTypeInfo, publishMethod, schemaRepository, messageRepository),
                    Subscribe = GenerateOperation(asyncApiTypeInfo, subscribeMethod, schemaRepository, messageRepository)
                };

                channels.Add(channelAttribute.Uri, channelItem);
            }

            return channels;
        }

        private Operation GenerateOperation(TypeInfo asyncApiTypeInfo, MethodInfo method, SchemaRepository schemaRepository, MessageRepository messageRepository)
        {
            if (method == null) return null;

            var operationAttribute = (OperationAttribute) method.GetCustomAttribute(typeof(OperationAttribute), true);

            var operation = new Operation
            {
                OperationId = operationAttribute.OperationId ?? asyncApiTypeInfo.FullName + $".{operationAttribute.Type}",
                Summary = operationAttribute.Summary ?? method.GetXmlDocsSummary(),
                Description = operationAttribute.Description ?? (method.GetXmlDocsRemarks() != "" ? method.GetXmlDocsRemarks() : null),
                Message = GenerateMessage(asyncApiTypeInfo, schemaRepository, messageRepository)
            };

            return operation;
        }

        private Reference GenerateMessage(TypeInfo channelsMarksAssembly, SchemaRepository schemaRepository, MessageRepository messageRepository)
        {
            var messageAttribute = (MessageAttribute) channelsMarksAssembly.GetCustomAttributes(typeof(MessageAttribute), true).Single();

            _schemaGenerator.GenerateSchema(messageAttribute.PayloadType, schemaRepository);
            messageRepository.GetOrAdd(messageAttribute.PayloadType, _options.SchemaIdSelector(messageAttribute.PayloadType), () =>
            {
                var message = new Message
                {
                Title = messageAttribute.Title,
                Name = messageAttribute.Name,
                Payload = new Reference(_options.SchemaIdSelector(messageAttribute.PayloadType), ReferenceType.Schema)
                };
                return @message;
            });
            var @ref = new Reference(_options.SchemaIdSelector(messageAttribute.PayloadType), ReferenceType.Message);

            // var message = new Message
            // {
            //     Payload = _schemaGenerator.GenerateSchema(messagePayloadAttribute.PayloadType, schemaRepository),
            //     // todo: all the other properties... message has a lot!
            // };

            return @ref;
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