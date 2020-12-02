using System.Threading.Tasks;

namespace KnstEventBus.Channels
{
    public interface IPubChannel : IChannel { }

    public interface IPubChannel<T> : IChannel<T>, IPubChannel
    {
        Task PublishAsync(T message);
    }
}