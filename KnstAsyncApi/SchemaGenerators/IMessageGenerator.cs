using System;
using KnstAsyncApi.Attributes;
using KnstAsyncApi.Schemas.V2;

namespace KnstAsyncApi.SchemaGenerators
{
    public interface IMessageGenerator
    {
        Message GenerateMessage(Type type, MessageRepository messageRepository, MessageAttribute messageAttribute);
    }
}