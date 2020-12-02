using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnstEventBus.Channels;

namespace KnstEventBus.Bus
{
    internal class SubBus : ISubBus
    {
        IEnumerable<ISubChannel> _subs;

        public SubBus(IEnumerable<ISubChannel> subs)
        {
            _subs = subs;
        }

        public Task SubscribeAsync()
        {
            return Task.WhenAll(_subs.Select(sub => sub.SubscribeAsync()));
        }

        public Task UnSubscribeAsync()
        {
            return Task.WhenAll(_subs.Select(sub => sub.UnSubscribeAsync()));
        }
    }
}