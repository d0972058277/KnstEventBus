using System;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChannelAttribute : Attribute
    {
        public string Ref { get; set; }
        public string Description { get; set; }

        public ChannelAttribute(string @ref)
        {
            Ref = @ref ??
                throw new ArgumentNullException(nameof(@ref));
        }
    }
}