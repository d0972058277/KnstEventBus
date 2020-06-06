using System.Collections.Generic;

namespace KnstAsyncApi.Schemas.V2 
{
    /// <summary>
    /// https://www.asyncapi.com/docs/specifications/2.0.0/#a-name-channelsobject-a-channels-object
    /// </summary>
    public class Channels : Dictionary<ChannelsFieldName, ChannelItem>
    {
        public void AddRange(Channels channels)
        {
            if (channels == null)
            {
                return;
            }
            
            foreach (var channel in channels)
            {
                Add(channel.Key, channel.Value);
            }
        }
    }
}