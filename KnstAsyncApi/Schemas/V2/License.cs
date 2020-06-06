using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-licenseobject-a-license-object
    /// </summary>
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