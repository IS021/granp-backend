using Granp.Models.Entities;
using Granp.Models.Enums;

namespace Granp.DTOs
{
    public class TimeSlotRequest
    {
        public WeekDay WeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}