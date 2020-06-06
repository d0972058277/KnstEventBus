using System;
using System.Text.RegularExpressions;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-componentsobject-a-components-object
    /// </summary>
    public class ComponentFieldName
    {
        private readonly string value;

        private const string ValidRegex = @"^[a-zA-Z0-9\.\-_]+$";

        public ComponentFieldName(string fieldName)
        {
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));
            if (!Regex.IsMatch(fieldName, ValidRegex)) throw new Exception($"component field name must match pattern {ValidRegex}");

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
            var componentFieldName = obj as ComponentFieldName;
            return componentFieldName != null && value.Equals(componentFieldName.value);
        }

        public static implicit operator ComponentFieldName(string s)
        {
            return new ComponentFieldName(s);
        }
    }
}