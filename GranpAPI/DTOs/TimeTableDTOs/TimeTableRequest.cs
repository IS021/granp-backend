using Granp.Models.Entities;

namespace Granp.DTOs
{
    public class TimeTableRequest
    {
        public int WeeksInAdvance { get; set; }
        public List<TimeSlotRequest> TimeSlots { get; set; } = null!;
    }
}