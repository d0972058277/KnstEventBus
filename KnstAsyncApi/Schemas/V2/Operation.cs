using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-operationobject-a-operation-object
    /// </summary>
    public class Operation
    {
        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();

        [JsonProperty("externalDocs")]
        public ExternalDocumentation ExternalDocs { get; set; }

        [JsonProperty("bindings")]
        public IDictionary<string, OperationBindings> Bindings { get; set; } = new Dictionary<string, OperationBindings>();

        [JsonProperty("message")]
        // public Message Message { get; set; }
        public Reference Message { get; set; }

        [JsonProperty("traits")]
        public IList<IOperationTrait> Traits { get; set; } = new List<IOperationTrait>();
    }
}