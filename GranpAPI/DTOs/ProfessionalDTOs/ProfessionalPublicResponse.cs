using Granp.Models.Enums;
using Granp.Models.Types;

namespace Granp.DTOs
{
    public class ProfessionalPublicResponse
    {
        // User Info
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!; // Is this needed?
        public string PhoneNumber { get; set; } = null!;
        
        // Profile Info
        public string? Description { get; set; }
        public Profession Profession { get; set; }
        public Address Address { get; set; } = null!;
        public int Age { get; set; }
        public bool IsVerified { get; set; }
        
        // Job Info
        public double HourlyRate { get; set; }
        public int MaxDistance { get; set; }
        public bool LongTimeJob { get; set; }
        public bool ShortTimeJob { get; set; }
    }
}