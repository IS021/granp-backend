using Granp.Models.Common;

namespace Granp.Models.Types
{
    // Should rapresent a week time table
    public class TimeTable : BaseEntity
    {
        public int WeeksInAdvance { get; set; }
        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();

        // Overlap percentage between this timetable and a list of timeslots
        public double Overlap(List<TimeSlot> slots)
        {
            double totalSlotsTime = slots.Sum(s => (s.EndTime - s.StartTime).TotalHours);
            double totalOverlapTime = 0;

            foreach (TimeSlot slot1 in slots)
            {
                foreach (TimeSlot slot2 in TimeSlots)
                {
                    if (TimeSlotsOverlap(slot1, slot2))
                    {
                        TimeSpan overlapTime = OverlapTime(slot1, slot2);
                        totalOverlapTime += overlapTime.TotalHours;
                    }
                }
            }

            return totalOverlapTime / totalSlotsTime;
        }

        // Check if this timetable has overlap with a list of timeslots
        public bool HasOverlap(List<TimeSlot> slots)
        {
            foreach (TimeSlot slot1 in slots)
            {
                foreach (TimeSlot slot2 in TimeSlots)
                {
                    if (TimeSlotsOverlap(slot1, slot2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Overlap condition
        private bool TimeSlotsOverlap(TimeSlot a, TimeSlot b)
        {
            return a.StartTime < b.EndTime && a.EndTime > b.StartTime && a.WeekDay == b.WeekDay;
        }

        // Overlap time between two timeslots
        private TimeSpan OverlapTime(TimeSlot a, TimeSlot b)
        {
            TimeSpan start = a.StartTime > b.StartTime ? a.StartTime : b.StartTime;
            TimeSpan end = a.EndTime < b.EndTime ? a.EndTime : b.EndTime;
            return end - start;
        }
    }
}