using System;
using System.Text.RegularExpressions;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-serversobject-a-servers-object
    /// </summary>
    public class ServersFieldName
    {
        private readonly string value;

        private const string ValidRegex = @"^[A-Za-z0-9_\-]+$";

        public ServersFieldName(string fieldName)
        {
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));
            if (!Regex.IsMatch(fieldName, ValidRegex)) throw new Exception($"servers field name must match pattern {ValidRegex}");

            value = fieldName;
        }

        public override string ToString()
        {
            return value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var serversFieldName = obj as ServersFieldName;
            return serversFieldName != null && value.Equals(serversFieldName.value);
        }

        public static implicit operator ServersFieldName(string s)
        {
            return new ServersFieldName(s);
        }
    }
}