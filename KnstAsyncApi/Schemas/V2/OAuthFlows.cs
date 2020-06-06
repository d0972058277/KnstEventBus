using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-oauthflowsobject-a-oauth-flows-object
    /// </summary>
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