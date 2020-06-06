using System.Collections.Generic;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-serversobject-a-servers-object
    /// </summary>
    public class Servers : Dictionary<ServersFieldName, Server> { }
}