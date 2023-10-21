using System.ComponentModel.DataAnnotations.Schema;

namespace Granp.Models.Types
{
    // Should rapresent a week time table
    public class TimeTable
    {
        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }
}