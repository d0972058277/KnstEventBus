using System.Threading;
using System.Threading.Tasks;

namespace KnstEventBus.Channels
{
    public interface ISubChannel : IChannel
    {
        Task SubscribeAsync();
        Task UnSubscribeAsync();
    }
}