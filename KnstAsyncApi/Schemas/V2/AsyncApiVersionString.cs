using System.Runtime.Serialization;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-a2sversionstring-a-asyncapi-version-string
    /// </summary>
    public enum AsyncApiVersionString
    {
        [EnumMember(Value = "2.0.0")]
        v2,
    }
}