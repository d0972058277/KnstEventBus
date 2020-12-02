using System.ComponentModel.DataAnnotations;
using KnstEventBus;

namespace Toys.Models
{
    /// <summary>
    /// Command a particular streetlight to dim the lights.
    /// </summary>
    public class DimLight
    {
        /// <summary>
        /// Percentage to which the light should be dimmed to.
        /// </summary>
        /// <value></value>
        [Range(0, 100)]
        public int Percentage { get; set; }
    }
}