using System.Collections.Generic;
using KnstAsyncApi.Schemas.V2.Abstracts;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-messageobject-a-message-object
    /// </summary>
    public class Message : IOperationMessage
    {
        [JsonProperty("headers")]
        public ISchema Headers { get; set; }

        [JsonProperty("payload")]
        public ISchema Payload { get; set; }

        [JsonProperty("correlationId")]
        public CorrelationId CorrelationId { get; set; }

        [JsonProperty("schemaFormat")]
        public string SchemaFormat { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("tags")]
        public ISet<Tag> Tags { get; set; } = new HashSet<Tag>();

        [JsonProperty("externalDocs")]
        public ExternalDocumentation ExternalDocs { get; set; }

        [JsonProperty("bindings")]
        public MessageBindings Bindings { get; set; }

        [JsonProperty("examples")]
        public IList<IDictionary<string, object>> Examples { get; set; } = new List<IDictionary<string, object>>();

        [JsonProperty("traits")]
        public IList<IMessageTrait> Traits { get; set; } = new List<IMessageTrait>();
    }
}