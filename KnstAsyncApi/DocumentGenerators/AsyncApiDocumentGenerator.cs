using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.SchemaGenerators;
using KnstAsyncApi.Schemas;
using KnstAsyncApi.Schemas.V2;
using KnstAsyncApi.Schemas.V2.Abstracts;
using Microsoft.Extensions.Options;
using Namotion.Reflection;

namespace KnstAsyncApi.DocumrntGenerations
{
    public class AsyncApiDocumentGenerator : IAsyncApiDocumentGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;
        private readonly ISchemaGenerator _schemaGenerator;
        private readonly IMessageGenerator _messageGenerator;

        public AsyncApiDocumentGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options, ISchemaGenerator schemaGenerator, IMessageGenerator messageGenerator)
        {
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
            _schemaGenerator = schemaGenerator;
            _messageGenerator = messageGenerator;
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
                var channelAttribute = (ChannelAttribute) asyncApiTypeInfo.GetCustomAttribute(typeof(ChannelAttribute), true);

                // OperationAttribute with class
                var publishAttribute = (PublishAttribute) asyncApiTypeInfo.GetCustomAttribute(typeof(PublishAttribute), true);
                var subscribeAttribute = (SubscribeAttribute) asyncApiTypeInfo.GetCustomAttribute(typeof(SubscribeAttribute), true);

                // OperationAttribute with method
                var publishMethods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(PublishAttribute), true) != null).ToList();
                var subscribeMethods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(SubscribeAttribute), true) != null).ToList();

                // Map to OperationAttribute
                var mapToPublishMethods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(MapToPublishAttribute), true) != null).ToList();
                var mapToSubscribeMethods = asyncApiTypeInfo.DeclaredMethods.Where(m => m.GetCustomAttribute(typeof(MapToSubscribeAttribute), true) != null).ToList();

                var channelItem = new ChannelItem();
                channelItem.Description = channelAttribute.Description ?? asyncApiTypeInfo.GetXmlDocsSummary();
                channelItem.Publish = GenerateOperation(asyncApiTypeInfo, publishAttribute, publishMethods, mapToPublishMethods, schemaRepository, messageRepository);
                channelItem.Subscribe = GenerateOperation(asyncApiTypeInfo, subscribeAttribute, subscribeMethods, mapToSubscribeMethods, schemaRepository, messageRepository);
                channels.Add(channelAttribute.Uri, channelItem);
            }

            return channels;
        }

        private Operation GenerateOperation<T>(TypeInfo asyncApiTypeInfo, T operationAttribute, IEnumerable<MethodInfo> operationMethods, IEnumerable<MethodInfo> mapToOperationMethods, SchemaRepository schemaRepository, MessageRepository messageRepository) where T : OperationAttribute
        {
            if (operationAttribute is null)
            {
                if (operationMethods.Count() > 1)
                    throw new Exception($"Method {typeof(T).Name} 只能有 1 個");

                if (mapToOperationMethods.Any())
                    throw new Exception($"使用 Method MapTo{typeof(T).Name} 來標記，需要使用 Class {typeof(T).Name} 來標記 Class");

                if (operationMethods.Any())
                {
                    var operation = operationMethods.Single();
                    return GenerateOperation(asyncApiTypeInfo, operation, schemaRepository, messageRepository);
                }

                return default(Operation);
            }
            else
            {
                // 檢查 Method 有沒有 Publish
                if (operationMethods.Any())
                    throw new Exception($"使用 Class {typeof(T).Name} 來標記，需要使用 MapTo{typeof(T).Name} 來標記 Method");

                return GenerateOperation(asyncApiTypeInfo, operationAttribute, mapToOperationMethods, schemaRepository, messageRepository);
            }
        }

        private Operation GenerateOperation(TypeInfo asyncApiTypeInfo, OperationAttribute operationAttribute, IEnumerable<MethodInfo> methods, SchemaRepository schemaRepository, MessageRepository messageRepository)
        {
            if (!methods.Any()) return null;

            var operation = new Operation
            {
                OperationId = operationAttribute.OperationId ?? asyncApiTypeInfo.FullName + $".{operationAttribute.Type}",
                Summary = operationAttribute.Summary,
                Description = operationAttribute.Description,
                Message = GenerateReferenceOfMessage(methods.SelectMany(method => (MessageAttribute[]) method.GetCustomAttributes(typeof(MessageAttribute), true)).ToArray(), schemaRepository, messageRepository)
            };

            return operation;
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
                Message = GenerateReferenceOfMessage((MessageAttribute[]) method.GetCustomAttributes(typeof(MessageAttribute), true), schemaRepository, messageRepository)
            };

            return operation;
        }

        private IOperationMessage GenerateReferenceOfMessage(IEnumerable<MessageAttribute> messageAttributes, SchemaRepository schemaRepository, MessageRepository messageRepository)
        {
            // https://www.asyncapi.com/docs/specifications/2.0.0#operationObject
            var messages = messageAttributes.Select(messageAttribute =>
            {
                var payloadType = messageAttribute.PayloadType;
                _schemaGenerator.GenerateSchema(payloadType, schemaRepository);
                _messageGenerator.GenerateMessage(payloadType, messageRepository, messageAttribute);
                return new Reference(_options.SchemaIdSelector(payloadType), ReferenceType.Message);
            }).ToList();

            if (messages.Count > 1)
            {
                return new MultipleMessage { OneOf = messages };
            }
            else
            {
                return messages.Single();
            }
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