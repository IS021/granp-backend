using Granp.Models.Entities;

namespace Granp.DTOs
{
    public class TimeTableResponse
    {
        public int WeeksInAdvance { get; set; }
        public List<TimeSlotResponse> TimeSlots { get; set; } = null!;
    }
}