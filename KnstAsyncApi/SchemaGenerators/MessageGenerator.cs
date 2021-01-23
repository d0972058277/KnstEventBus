using System;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.DocumrntGenerations;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;
using Namotion.Reflection;

namespace KnstAsyncApi.SchemaGenerators
{
    public class MessageGenerator : IMessageGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;
        private readonly IDataContractResolver _dataContractResolver;

        public MessageGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options, IDataContractResolver dataContractResolver)
        {
            _options = options.Value ??
                throw new ArgumentNullException(nameof(options));
            _dataContractResolver = dataContractResolver;
        }

        public Message GenerateMessage(Type type, MessageRepository messageRepository, MessageAttribute messageAttribute)
        {
            return messageRepository.GetOrAdd(type, _options.SchemaIdSelector(type), () =>
            {
                var dataContract = _dataContractResolver.GetDataContractForType(type);
                var message = new Message
                {
                    Title = messageAttribute.Title,
                    Name = messageAttribute.Name
                };

                if (dataContract.DataType == DataType.Array)
                {
                    message.Summary = dataContract.ArrayItemType.GetXmlDocsSummary();
                    message.Payload = new Schema { Type = "array", Items = new Reference(_options.SchemaIdSelector(dataContract.ArrayItemType), ReferenceType.Schema) };
                }
                else
                {
                    message.Summary = type.GetXmlDocsSummary();
                    message.Payload = new Reference(_options.SchemaIdSelector(type), ReferenceType.Schema);
                }

                return message;
            });
        }
    }
}