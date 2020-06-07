using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using KnstAsyncApi.Generations;
using KnstAsyncApi.Schemas.V2;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace KnstAsyncApi.Generations.SchemaGeneration
{
    public class SchemaGenerator : ISchemaGenerator
    {
        private readonly AsyncApiDocumentGeneratorOptions _options;

        public SchemaGenerator(IOptions<AsyncApiDocumentGeneratorOptions> options)
        {
            _options = options.Value ??
                throw new ArgumentNullException(nameof(options));
        }

        public ISchema GenerateSchema(Type type, ISchemaRepository schemaRepository)
        {
            var schemaId = _options.SchemaIdSelector(type);

            var reference = schemaRepository.GetOrAdd(type, schemaId, () => TypeSchemaFactory(type, schemaRepository));

            return reference;
        }

        private Schema TypeSchemaFactory(Type type, ISchemaRepository schemaRepository)
        {
            var schema = GetSchemaIfPrimitive(type);
            if (schema != null)
            {
                return schema;
            }

            schema = GetSchemaIfEnumerable(type, schemaRepository);
            if (schema != null)
            {
                return schema;
            }

            var propertyAndFieldMembers = type.GetProperties().Cast<MemberInfo>()
                .Concat(type.GetFields()).ToArray();

            return CreateSchemaFromPropertyAndFieldMembers(schemaRepository, propertyAndFieldMembers);
        }

        private Schema CreateSchemaFromPropertyAndFieldMembers(ISchemaRepository schemaRepository, MemberInfo[] propertyAndFieldMembers)
        {
            var requiredMembers = new HashSet<string>();
            var schema = new Schema
            {
                Properties = new Dictionary<string, ISchema>()
            };

            foreach (var member in propertyAndFieldMembers)
            {
                var underlyingTypeOfMember = ReflectionExtensions.GetUnderlyingType(member);
                var memberName = GetMemberName(member);

                ISchema memberSchema = GetSchemaIfPrimitive(underlyingTypeOfMember);

                if (memberSchema == null)
                {
                    memberSchema = GetSchemaIfEnumerable(underlyingTypeOfMember, schemaRepository);
                    if (memberSchema != null && memberSchema is Schema s1) // todo: this better
                    {
                        s1.MinItems = member.GetMinItems();
                        s1.MaxItems = member.GetMaxItems();
                        s1.UniqueItems = member.GetIsUniqueItems();
                    }

                    if (memberSchema == null)
                    {
                        memberSchema = GenerateSchema(underlyingTypeOfMember, schemaRepository);
                    }
                }

                if (memberSchema is Schema s2) // todo: this means we won't get anything on reference types.... is this okay???
                {
                    s2.Title = s2.Title ?? member.GetTitle();
                    s2.Description = s2.Description ?? member.GetDescription();
                    s2.Minimum = s2.Minimum ?? member.GetMinimum();
                    s2.Maximum = s2.Maximum ?? member.GetMaximum();
                    s2.MinLength = s2.MinLength ?? member.GetMinLength();
                    s2.MaxLength = s2.MaxLength ?? member.GetMaxLength();
                    s2.Pattern = s2.Pattern ?? member.GetPattern();
                    s2.Example = s2.Example ?? member.GetExample();

                    if (member.GetIsRequired())
                    {
                        requiredMembers.Add(member.Name);
                    }
                }

                schema.Properties.Add(memberName, memberSchema);
            }

            if (requiredMembers.Count > 0)
            {
                schema.Required = requiredMembers;
            }

            return schema;
        }

        public string GetMemberName(MemberInfo member)
        {
            var jsonPropertyAttribute = member.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonPropertyAttribute?.PropertyName != null)
            {
                return jsonPropertyAttribute.PropertyName;
            }

            var dataMemberAttribute = member.GetCustomAttribute<DataMemberAttribute>();
            if (dataMemberAttribute?.Name != null)
            {
                return dataMemberAttribute.Name;
            }

            return member.Name;
        }

        private Schema GetSchemaIfPrimitive(Type type)
        {
            if (type.IsInteger())
            {
                return new Schema { Type = "integer" };
            }

            if (type.IsNumber())
            {
                return new Schema { Type = "number" };
            }

            if (type == typeof(string))
            {
                return new Schema { Type = "string" };
            }

            if (type == typeof(Guid))
            {
                return new Schema
                {
                Type = "string",
                Pattern = "/^[0-9A-Fa-f]{8}(?:-[0-9A-Fa-f]{4}){3}-[0-9A-Fa-f]{12}$/",
                Example = "438c8c86-4d50-4e96-b623-c25633933108"
                };
            }

            if (type.IsBoolean())
            {
                return new Schema { Type = "boolean" };
            }

            if (type.IsEnum(out var members))
            {
                return new Schema
                {
                    Type = "string",
                        Enum = members,
                };
            }

            if (type.IsDateTime())
            {
                return new Schema
                {
                    Type = "string",
                        Format = "date-time",
                };
            }

            if (type.IsTimeSpan())
            {
                return new Schema
                {
                    Type = "string",
                        Format = "time-span"
                };
            }

            return null;
        }

        private Schema GetSchemaIfEnumerable(Type type, ISchemaRepository schemaRepository)
        {
            if (type.IsEnumerable(out var elementType))
            {
                var schema = new Schema
                {
                    Type = "array",
                    Items = GenerateSchema(elementType, schemaRepository),
                };

                return schema;
            }

            return null;
        }
    }
}