using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using KnstAsyncApi.DocumrntGenerations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace KnstAsyncApi.SchemaGenerators
{
    public class NewtonsoftDataContractResolver : IDataContractResolver
    {
        private readonly AsyncApiDocumentGeneratorOptions _generatorOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IContractResolver _contractResolver;

        public NewtonsoftDataContractResolver(IOptions<AsyncApiDocumentGeneratorOptions> options, JsonSerializerSettings serializerSettings)
        {
            _generatorOptions = options.Value;
            _serializerSettings = serializerSettings;
            _contractResolver = serializerSettings.ContractResolver ?? new DefaultContractResolver();
        }

        public DataContract GetDataContractForType(Type type)
        {
            var jsonContract = _contractResolver.ResolveContract(type.IsNullable(out Type innerType) ? innerType : type);

            if (jsonContract is JsonPrimitiveContract && !jsonContract.UnderlyingType.IsEnum)
            {
                var primitiveTypeAndFormat = PrimitiveTypesAndFormats.ContainsKey(jsonContract.UnderlyingType) ?
                    PrimitiveTypesAndFormats[jsonContract.UnderlyingType] :
                    Tuple.Create(DataType.String, (string) null);

                return new DataContract(
                    dataType: primitiveTypeAndFormat.Item1,
                    format: primitiveTypeAndFormat.Item2,
                    underlyingType: jsonContract.UnderlyingType);
            }

            if (jsonContract is JsonPrimitiveContract && jsonContract.UnderlyingType.IsEnum)
            {
                var enumValues = GetSerializedEnumValuesFor(jsonContract);

                var primitiveTypeAndFormat = (enumValues.Any(value => value.GetType() == typeof(string))) ?
                    PrimitiveTypesAndFormats[typeof(string)] :
                    PrimitiveTypesAndFormats[jsonContract.UnderlyingType.GetEnumUnderlyingType()];

                return new DataContract(
                    dataType: primitiveTypeAndFormat.Item1,
                    format: primitiveTypeAndFormat.Item2,
                    underlyingType: jsonContract.UnderlyingType,
                    enumValues: enumValues);
            }

            if (jsonContract is JsonDictionaryContract jsonDictionaryContract)
            {
                var keyType = jsonDictionaryContract.DictionaryKeyType ?? typeof(object);
                var valueType = jsonDictionaryContract.DictionaryValueType ?? typeof(object);

                if (keyType.IsEnum)
                {
                    // This is a special case where we can include named properties based on the enum values
                    var enumValues = GetDataContractForType(keyType).EnumValues;

                    var propertyNames = enumValues.Any(value => value is string) ?
                        enumValues.Cast<string>() :
                        keyType.GetEnumNames();

                    return new DataContract(
                        dataType: DataType.Object,
                        underlyingType: jsonDictionaryContract.UnderlyingType,
                        properties: propertyNames.Select(name => new DataProperty(name, valueType)));
                }

                return new DataContract(
                    dataType: DataType.Object,
                    underlyingType: jsonDictionaryContract.UnderlyingType,
                    additionalPropertiesType: valueType);
            }

            if (jsonContract is JsonArrayContract jsonArrayContract)
            {
                return new DataContract(
                    dataType: DataType.Array,
                    underlyingType: jsonArrayContract.UnderlyingType,
                    arrayItemType: jsonArrayContract.CollectionItemType ?? typeof(object));
            }

            if (jsonContract is JsonObjectContract jsonObjectContract)
            {
                return new DataContract(
                    dataType: DataType.Object,
                    underlyingType: jsonObjectContract.UnderlyingType,
                    properties: GetDataPropertiesFor(jsonObjectContract),
                    additionalPropertiesType: jsonObjectContract.ExtensionDataValueType);
            }

            if (jsonContract.UnderlyingType == typeof(JArray))
            {
                return new DataContract(
                    dataType: DataType.Array,
                    underlyingType: jsonContract.UnderlyingType,
                    arrayItemType: typeof(JToken));
            }

            if (jsonContract.UnderlyingType == typeof(JObject))
            {
                return new DataContract(
                    dataType: DataType.Object,
                    underlyingType: jsonContract.UnderlyingType);
            }

            return new DataContract(
                dataType: DataType.Unknown,
                underlyingType: jsonContract.UnderlyingType);
        }

        private IEnumerable<object> GetSerializedEnumValuesFor(JsonContract jsonContract)
        {
            var stringEnumConverter = (jsonContract.Converter as StringEnumConverter) ??
                _serializerSettings.Converters.OfType<StringEnumConverter>().FirstOrDefault();

            if (stringEnumConverter == null)
            {
                stringEnumConverter = new StringEnumConverter();
            }

            if (stringEnumConverter != null)
            {
                return jsonContract.UnderlyingType.GetMembers(BindingFlags.Public | BindingFlags.Static)
                    .Select(member =>
                    {
                        var memberAttribute = member.GetCustomAttributes<EnumMemberAttribute>().FirstOrDefault();
                        return GetConvertedEnumName((memberAttribute?.Value ?? member.Name), (memberAttribute?.Value != null), stringEnumConverter);
                    })
                    .ToList();
            }

            return jsonContract.UnderlyingType.GetEnumValues()
                .Cast<object>();
        }

#if NETCOREAPP3_0
        private string GetConvertedEnumName(string enumName, bool hasSpecifiedName, StringEnumConverter stringEnumConverter)
        {
            if (stringEnumConverter.NamingStrategy != null)
                return stringEnumConverter.NamingStrategy.GetPropertyName(enumName, hasSpecifiedName);

            return (stringEnumConverter.CamelCaseText) ?
                new CamelCaseNamingStrategy().GetPropertyName(enumName, hasSpecifiedName) :
                enumName;
        }
#else
        private string GetConvertedEnumName(string enumName, bool hasSpecifiedName, StringEnumConverter stringEnumConverter)
        {
            return (stringEnumConverter.CamelCaseText) ?
                new CamelCaseNamingStrategy().GetPropertyName(enumName, hasSpecifiedName) :
                enumName;
        }
#endif

        private IEnumerable<DataProperty> GetDataPropertiesFor(JsonObjectContract jsonObjectContract)
        {
            if (jsonObjectContract.UnderlyingType == typeof(object))
            {
                return null;
            }

            var dataProperties = new List<DataProperty>();

            foreach (var jsonProperty in jsonObjectContract.Properties)
            {
                if (jsonProperty.Ignored) continue;

                var required = jsonProperty.IsRequiredSpecified() ?
                    jsonProperty.Required :
                    jsonObjectContract.ItemRequired ?? Required.Default;

                var isSetViaConstructor = jsonProperty.DeclaringType.GetConstructors()
                    .SelectMany(c => c.GetParameters())
                    .Any(p => string.Equals(p.Name, jsonProperty.PropertyName, StringComparison.OrdinalIgnoreCase));

                jsonProperty.TryGetMemberInfo(out MemberInfo memberInfo);

                dataProperties.Add(
                    new DataProperty(
                        name: jsonProperty.PropertyName,
                        isRequired: (required == Required.Always || required == Required.AllowNull),
                        isNullable: (required == Required.AllowNull || required == Required.Default) && jsonProperty.PropertyType.IsReferenceOrNullableType(),
                        isReadOnly : jsonProperty.Readable && !jsonProperty.Writable && !isSetViaConstructor,
                        isWriteOnly : jsonProperty.Writable && !jsonProperty.Readable,
                        memberType : jsonProperty.PropertyType,
                        memberInfo : memberInfo));
            }

            return dataProperties;
        }

        private static readonly Dictionary<Type, Tuple<DataType, string>> PrimitiveTypesAndFormats = new Dictionary<Type, Tuple<DataType, string>>
        {
            [typeof(bool)] = Tuple.Create(DataType.Boolean, (string) null),
            [typeof(byte)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(sbyte)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(short)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(ushort)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(int)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(uint)] = Tuple.Create(DataType.Integer, "int32"),
            [typeof(long)] = Tuple.Create(DataType.Integer, "int64"),
            [typeof(ulong)] = Tuple.Create(DataType.Integer, "int64"),
            [typeof(float)] = Tuple.Create(DataType.Number, "float"),
            [typeof(double)] = Tuple.Create(DataType.Number, "double"),
            [typeof(decimal)] = Tuple.Create(DataType.Number, "double"),
            [typeof(byte[])] = Tuple.Create(DataType.String, "byte"),
            [typeof(string)] = Tuple.Create(DataType.String, (string) null),
            [typeof(char)] = Tuple.Create(DataType.String, (string) null),
            [typeof(DateTime)] = Tuple.Create(DataType.String, "date-time"),
            [typeof(DateTimeOffset)] = Tuple.Create(DataType.String, "date-time"),
            [typeof(Guid)] = Tuple.Create(DataType.String, "uuid"),
            [typeof(Uri)] = Tuple.Create(DataType.String, "uri"),
            [typeof(TimeSpan)] = Tuple.Create(DataType.String, "time-span")
        };
    }
}