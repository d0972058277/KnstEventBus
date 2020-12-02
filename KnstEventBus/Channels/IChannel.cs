using System;
using System.Threading.Tasks;

namespace KnstEventBus.Channels
{
    public interface IChannel { }
    public interface IChannel<T> : IChannel { }
}