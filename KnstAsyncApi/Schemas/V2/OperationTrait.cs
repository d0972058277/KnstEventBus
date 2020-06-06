using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// Can be either an <see cref="OperationTrait"/> or a <see cref="Reference"/> to an operation trait.
    /// </summary>
    public interface IOperationTrait { }

    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-operationtraitobject-a-operation-trait-object
    /// </summary>    
    public class OperationTrait : IOperationTrait
    {
        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public ISet<Tag> Tags { get; set; }

        [JsonProperty("externalDocs")]
        public ExternalDocumentation ExternalDocs { get; set; }

        [JsonProperty("bindings")]
        public IDictionary<string, OperationBindings> Bindings { get; set; }
    }
}