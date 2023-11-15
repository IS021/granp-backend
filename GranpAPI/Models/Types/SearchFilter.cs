using Granp.Models.Enums;
using Granp.Models.Entities;

namespace Granp.Models.Types 
{
    public class SearchFilter
    {
        public Location Location { get; set; } = null!;
        public Profession? Profession { get; set; }
        public List<TimeSlot>? TimeSlots { get; set; }
        public double? MaxHourlyRate { get; set; }
        public bool? LongTimeJob { get; set; }
        public bool? ShortTimeJob { get; set; }
        public int? MaxDistance { get; set; }
        public int? MaxWeeksInAdvance { get; set; }
        public float? MinRating { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}