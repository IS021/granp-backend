using Granp.Models.Enums;
using Granp.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Granp.Models.Entities
{
    public class TimeSlot : BaseEntity
    {
        public WeekDay WeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
