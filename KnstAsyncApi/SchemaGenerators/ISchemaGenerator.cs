using System;
using System.Reflection;
using KnstAsyncApi.Schemas.V2;

namespace KnstAsyncApi.SchemaGenerators
{
    public interface ISchemaGenerator
    {
        /// <summary>
        /// Generate a schema, save it in the <paramref name="schemaRepository"/>, and return a reference to it.
        /// </summary>
        ISchema GenerateSchema(Type type, SchemaRepository schemaRepository, MemberInfo memberInfo = null, ParameterInfo parameterInfo = null);
    }
}