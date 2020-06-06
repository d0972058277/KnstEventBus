using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2 {
    public class License
    {
        public License(string name)
        {
            Name = name;
        }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}