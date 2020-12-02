using System.Threading;
using System.Threading.Tasks;
using KnstEventBus.Bus;
using Microsoft.Extensions.Hosting;

namespace KnstEventBus
{
    public class SubscribeBackgroundService : BackgroundService
    {
        ISubBus _subBus;

        public SubscribeBackgroundService(ISubBus subBus)
        {
            _subBus = subBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _subBus.SubscribeAsync();
            stoppingToken.Register(() => _subBus.UnSubscribeAsync().Wait());
        }
    }
}