using System.ComponentModel.DataAnnotations.Schema;

namespace Granp.Models.Types
{
    public class TimeSlot
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}