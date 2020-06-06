using System.Collections.Generic;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-messagebindingsobject-a-message-bindings-object
    /// </summary>
    public class MessageBindings : Dictionary<MessageBindingsFieldName, IMessageBinding> { }
}