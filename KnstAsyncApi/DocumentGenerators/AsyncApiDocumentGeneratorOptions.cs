using System;
using System.Reflection;
using KnstAsyncApi.Schemas.V2;
using Newtonsoft.Json.Serialization;

namespace KnstAsyncApi.DocumrntGenerations
{
    public class AsyncApiDocumentGeneratorOptions
    {
        /// <summary>
        /// The base asyncapi schema.
        /// This will be augmented with other information auto-discovered from attributes.
        /// </summary>
        public AsyncApiDocument AsyncApi { get; set; } = new AsyncApiDocument();

        /// <summary>
        /// A list of marker types from assemblies to scan for Saunter attributes.
        /// </summary>
        // public IList<Type> AssemblyMarkerTypes { get; set; } = new List<Type>();

        /// <summary>
        /// A function to select a schemaId for a type.
        /// </summary>
        public Func<Type, string> SchemaIdSelector { get; set; } = type => new CamelCaseNamingStrategy().GetPropertyName(type.Name, false);

        /// <summary>
        /// A function to select the name for a property.
        /// </summary>
        public Func<PropertyInfo, string> PropertyNameSelector { get; set; } = prop => new CamelCaseNamingStrategy().GetPropertyName(prop.Name, false);

        /// <summary>
        /// A list of filters that will be applied to the generated AsyncAPI document.
        /// </summary>
        // public IList<IDocumentFilter> DocumentFilters { get; } = new List<IDocumentFilter>();

        /// <summary>
        /// A list of filters that will be applies to any generated channels.
        /// </summary>
        // public IList<IChannelItemFilter> ChannelItemFilters { get; } = new List<IChannelItemFilter>();

        /// <summary>
        /// A list of filters that will be applied to any generated Publish operations.
        /// </summary>
        // public IList<OperationFilter> OperationFilters { get; } = new List<OperationFilter>();

        // 參考 swagger 加的
        public bool UseInlineDefinitionsForEnums { get; set; } = true;
        public bool IgnoreObsoleteProperties { get; set; } = true;
        public Func<Type, string> DiscriminatorSelector { get; set; } = (Type type) => "$type";
    }
}