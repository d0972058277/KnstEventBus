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

        public MessageGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options)
        {
            _options = options.Value;
        }

        public Message GenerateMessage(Type type, MessageRepository messageRepository, MessageAttribute messageAttribute)
        {
            return messageRepository.GetOrAdd(type, _options.SchemaIdSelector(type), () =>
            {
                var message = new Message
                {
                Title = messageAttribute.Title,
                Name = messageAttribute.Name,
                Summary = type.GetXmlDocsSummary(),
                Payload = new Reference(_options.SchemaIdSelector(type), ReferenceType.Schema)
                };
                return message;
            });
        }
    }
}