using System;

namespace KnstAsyncApi.Schemas.V2
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-channelsobject-a-channels-object
    /// </summary>
    public class ChannelsFieldName
    {
        private readonly Uri value;

        public ChannelsFieldName(string fieldName)
        {
            if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));

            value = new Uri(fieldName, UriKind.Relative);
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
            var channelsFieldName = obj as ChannelsFieldName;
            return channelsFieldName != null && value.Equals(channelsFieldName.value);
        }

        public static implicit operator ChannelsFieldName(string s)
        {
            return new ChannelsFieldName(s);
        }
    }
}