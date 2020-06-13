using System.ComponentModel.DataAnnotations;
using KnstEventBus;

namespace Toys.Models
{
    /// <summary>
    /// Command a particular streetlight to turn the lights on or off.
    /// </summary>
    public class TurnOnOff : IntegrationEvent
    {
        [Required]
        public string StreetlightId { get; set; }

        /// <summary>
        /// Whether to turn on or off the light.
        /// </summary>
        /// <value></value>
        public TurnOnOffCommand Command { get; set; }
    }

    public enum TurnOnOffCommand
    {
        On,
        Off
    }
}