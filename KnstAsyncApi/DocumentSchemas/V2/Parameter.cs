using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2 {
    public class Parameter 
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("schema")]
        public Schema Schema { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}