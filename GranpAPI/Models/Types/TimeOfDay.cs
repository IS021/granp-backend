using System.ComponentModel.DataAnnotations;

namespace Granp.Models.Types
{
    public struct TimeOfDay
    {
        [Range(0, 23)]
        public int hours { get; set; }

        [Range(0, 59)]
        public int minutes { get; set; }
    }
}