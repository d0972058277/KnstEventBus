using Newtonsoft.Json;

namespace KnstAsyncApi.DocumentSchemas.V2 {
    public class OAuthFlows
    {
        [JsonProperty("implicit")]
        public OAuthFlow Implicit { get; set; }

        [JsonProperty("password")]
        public OAuthFlow Password { get; set; }

        [JsonProperty("clientCredentials")]
        public OAuthFlow ClientCredentials { get; set; }

        [JsonProperty("authorizationCode")]
        public OAuthFlow AuthorizationCode { get; set; }

    }
}