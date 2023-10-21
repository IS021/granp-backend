using System.Data;
using Granp.Models.Common;
using Granp.Models.Types;
using Granp.Models.Enums;

namespace Granp.Models.Entities
{
    public class Professional : BaseUser
    {
        // Profile Info
        public string? Description { get; set; }
        public Profession Profession { get; set; }
        public Address Address { get; set; } = null!;
        
        // Reviews (these are common to both Customer and Professional) -> Rateable User ?
        public ICollection<CustomerReview>? WrittenReviews { get; set; }
        public ICollection<ProfessionalReview>? ReceivedReviews { get; set; }
        public int NumberOfReviews { get; set; }
        public float Rating { get; set; }
        
        // Job Info
        public TimeTable TimeTable { get; set; } = new TimeTable();
        public double HourlyRate { get; set; }
        public int MaxDistance { get; set; }
        public int MaxWeeksInAdvance { get; set; }
        public bool LongTimeJob { get; set; }
        public bool ShortTimeJob { get; set; }

    }
}