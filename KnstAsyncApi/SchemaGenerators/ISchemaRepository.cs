using System;
using System.Collections.Generic;
using KnstAsyncApi.Schemas.V2;

namespace KnstAsyncApi.SchemaGenerations
{
    public interface ISchemaRepository
    {
        IDictionary<ComponentFieldName, Schema> Schemas { get; }

        ISchema GetOrAdd(Type type, string schemaId, Func<Schema> factory);
    }
}