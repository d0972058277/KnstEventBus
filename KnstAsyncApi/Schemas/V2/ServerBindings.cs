using System.Collections.Generic;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-serverbindingsobject-a-server-bindings-object
    /// </summary>
    public class ServerBindings : Dictionary<ServerBindingsFieldName, IServerBinding> { }
}