using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using KnstAsyncApi.DocumrntGenerations;
using KnstAsyncApi.SchemaGenerators;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnstAsyncApi.SchemaGenerators
{
    public class SchemaGenerator : ISchemaGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;
        private readonly IDataContractResolver _dataContractResolver;

        public SchemaGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options, IDataContractResolver dataContractResolver)
        {
            _options = options.Value ??
                throw new ArgumentNullException(nameof(options));
            _dataContractResolver = dataContractResolver;
        }

        public ISchema GenerateSchema(Type type, SchemaRepository schemaRepository, MemberInfo memberInfo = null, ParameterInfo parameterInfo = null)
        {
            var schema = GenerateSchemaForType(type, schemaRepository);

            return schema;
        }

        private ISchema GenerateSchemaForType(Type type, SchemaRepository schemaRepository)
        {
            var schemaId = _options.SchemaIdSelector(type);
            var dataContract = _dataContractResolver.GetDataContractForType(type);
            var shouldBeReferenced =
                // regular object
                (dataContract.DataType == DataType.Object && dataContract.Properties != null && !dataContract.UnderlyingType.IsDictionary()) ||
                // dictionary-based AND self-referencing
                (dataContract.DataType == DataType.Object && dataContract.AdditionalPropertiesType == dataContract.UnderlyingType) ||
                // array-based AND self-referencing
                (dataContract.DataType == DataType.Array && dataContract.ArrayItemType == dataContract.UnderlyingType) ||
                // enum-based AND opted-out of inline
                (dataContract.EnumValues != null && !_options.UseInlineDefinitionsForEnums);

            var schema = shouldBeReferenced ? GenerateReferencedSchema(dataContract, schemaRepository) : GenerateInlineSchema(dataContract, schemaRepository);

            //var schema = schemaRepository.GetOrAdd(type, schemaId, () => TypeSchemaFactory(type, schemaRepository));

            return schema;
        }

        private ISchema GenerateReferencedSchema(DataContract dataContract, SchemaRepository schemaRepository)
        {
            return schemaRepository.GetOrAdd(
                dataContract.UnderlyingType,
                _options.SchemaIdSelector(dataContract.UnderlyingType),
                () =>
                {
                    var schema = GenerateInlineSchema(dataContract, schemaRepository);
                    //ApplyFilters(schema, dataContract.UnderlyingType, schemaRepository);
                    return schema;
                });
        }

        private Schema GenerateInlineSchema(DataContract dataContract, SchemaRepository schemaRepository)
        {
            if (dataContract.DataType == DataType.Unknown)
                return new Schema();

            if (dataContract.DataType == DataType.Object)
                return GenerateObjectSchema(dataContract, schemaRepository);

            if (dataContract.DataType == DataType.Array)
                return GenerateArraySchema(dataContract, schemaRepository);

            else
                return GeneratePrimitiveSchema(dataContract);
        }

        private Schema GenerateObjectSchema(DataContract dataContract, SchemaRepository schemaRepository)
        {
            var schema = new Schema
            {
                Type = "object",
                Properties = new Dictionary<string, ISchema>(),
                Required = new SortedSet<string>(),
            };

            foreach (var dataProperty in dataContract.Properties ?? Enumerable.Empty<DataProperty>())
            {
                var customAttributes = dataProperty.MemberInfo?.GetCustomAttributes(true);

                if (_options.IgnoreObsoleteProperties && customAttributes.OfType<ObsoleteAttribute>().Any())
                    continue;

                schema.Properties[dataProperty.Name] = GeneratePropertySchema(dataProperty, schemaRepository);

                if (dataProperty.IsRequired || customAttributes.OfType<System.ComponentModel.DataAnnotations.RequiredAttribute>().Any())
                    schema.Required.Add(dataProperty.Name);
            }

            return schema;
        }
        private ISchema GeneratePropertySchema(DataProperty serializerMember, SchemaRepository schemaRepository)
        {
            var schema = GenerateSchemaForType(serializerMember.MemberType, schemaRepository);

            return schema;
        }

        private Schema GenerateArraySchema(DataContract dataContract, SchemaRepository schemaRepository)
        {
            return new Schema
            {
                Type = "array",
                Items = GenerateSchema(dataContract.ArrayItemType, schemaRepository),
                UniqueItems = dataContract.UnderlyingType.IsSet() ? (bool?)true : null
            };
        }

        private Schema GeneratePrimitiveSchema(DataContract dataContract)
        {
            var schema = new Schema
            {
                Type = dataContract.DataType.ToString().ToLower(CultureInfo.InvariantCulture),
                Format = dataContract.Format
            };

            if (dataContract.EnumValues != null)
            {
                schema.Type = DataType.String.ToString().ToLower(CultureInfo.InvariantCulture);
                schema.Format = null;
                schema.Enum = dataContract.EnumValues
                    .Distinct()
                    .Select(value => value.ToString()) // ??êË®≠ Enum ËΩâÂ?ó‰∏≤
                    .ToList();
            }

            return schema;
        }
    }
}