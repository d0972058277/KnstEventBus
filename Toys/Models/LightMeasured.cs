using System.ComponentModel.DataAnnotations;
using KnstEventBus;

namespace Toys.Models
{
    /// <summary>
    /// Inform about environmental lighting conditions for a particular streetlight.
    /// </summary>
    public class LightMeasured
    {
        [Required]
        public string StreetlightId { get; set; }

        [Range(0, int.MaxValue)]
        public int Lumens { get; set; }
    }
}