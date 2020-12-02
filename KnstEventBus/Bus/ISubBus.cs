using System.Threading.Tasks;

namespace KnstEventBus.Bus
{
    public interface ISubBus
    {
        Task SubscribeAsync();
        Task UnSubscribeAsync();
    }
}