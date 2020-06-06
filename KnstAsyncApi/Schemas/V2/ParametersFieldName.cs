using System;
using System.Text.RegularExpressions;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-parametersobject-a-parameters-object
    /// </summary>
    public class ParametersFieldName
    {
        private readonly string value;

        private const string ValidRegex = @"^[A-Za-z0-9_\-]+$";

        public ParametersFieldName(string fieldName)
        {
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));
            if (!Regex.IsMatch(fieldName, ValidRegex)) throw new Exception($"parameter field name must match pattern {ValidRegex}");

            value = fieldName;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var parametersFieldName = obj as ParametersFieldName;
            return parametersFieldName != null && value.Equals(parametersFieldName.value);
        }
    }
}