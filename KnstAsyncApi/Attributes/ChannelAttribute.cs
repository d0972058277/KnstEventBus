using System;
using KnstAsyncApi.Attributes.Marks;

namespace KnstAsyncApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChannelAttribute : Attribute, IChannelsMark
    {
        public string Uri { get; set; }
        public string Description { get; set; }

        public ChannelAttribute(string uri)
        {
            Uri = uri ??
                throw new ArgumentNullException(nameof(uri));
        }
    }
}