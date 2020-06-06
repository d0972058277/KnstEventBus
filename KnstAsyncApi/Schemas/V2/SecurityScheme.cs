using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-securityschemeobject-a-security-scheme-object
    /// </summary>
    public class SecurityScheme
    {
        public SecurityScheme(SecuritySchemeType type)
        {
            Type = type;
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SecuritySchemeType Type { get; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("in")]
        public string In { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("bearerFormat")]
        public string BearerFormat { get; set; }

        [JsonProperty("flows")]
        public OAuthFlows Flows { get; set; }

        [JsonProperty("openIdConnectUrl")]
        public string OpenIdConnectUrl { get; set; }
    }
}