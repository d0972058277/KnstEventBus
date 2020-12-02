using System.Collections.Generic;
using KnstAsyncApi.Schemas.V2.Abstracts;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    public class MultipleMessage : IOperationMessage
    {
        [JsonProperty("oneOf")]
        public IList<Reference> OneOf { get; set; } = new List<Reference>();
    }
}