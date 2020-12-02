using System.Threading.Tasks;

namespace KnstEventBus.Bus
{
    public interface IPubBus
    {
        Task PublishAsync<T>(T message);
    }
}