using System.Collections.Generic;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-securityrequirementobject-a-security-requirement-object
    /// </summary>
    public class SecurityRequirement : Dictionary<string, List<string>> { }
}