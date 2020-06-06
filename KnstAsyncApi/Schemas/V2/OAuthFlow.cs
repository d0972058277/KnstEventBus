using System.Collections.Generic;
using Newtonsoft.Json;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-oauthflowobject-a-oauth-flow-object
    /// </summary>
    public class OAuthFlow
    {
        [JsonProperty("authorizationUrl")]
        public string AuthorizationUrl { get; set; }

        [JsonProperty("tokenUrl")]
        public string TokenUrl { get; set; }

        [JsonProperty("refreshUrl")]
        public string RefreshUrl { get; set; }

        [JsonProperty("scopes")]
        public IDictionary<string, string> Scopes { get; set; }
    }
}