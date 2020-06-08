using System;
using System.Reflection;
using KnstAsyncApi.Schemas.V2;

namespace KnstAsyncApi.SchemaGenerations
{
    public interface ISchemaGenerator
    {
        /// <summary>
        /// Generate a schema, save it in the <paramref name="schemaRepository"/>, and return a reference to it.
        /// </summary>
        ISchema GenerateSchema(Type type, ISchemaRepository schemaRepository, MemberInfo memberInfo = null, ParameterInfo parameterInfo = null);
    }
}