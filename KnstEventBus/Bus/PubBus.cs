using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using KnstEventBus.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace KnstEventBus.Bus
{
    internal class PubBus : IPubBus
    {
        Dictionary<Type, List<IPubChannel>> _pubs;

        public PubBus(Dictionary<Type, List<IPubChannel>> pubs)
        {
            _pubs = pubs;
        }

        public Task PublishAsync<T>(T message)
        {
            return Task.WhenAll(_pubs[typeof(T)].Select(pub => (pub as IPubChannel<T>).PublishAsync(message)));
        }
    }
}