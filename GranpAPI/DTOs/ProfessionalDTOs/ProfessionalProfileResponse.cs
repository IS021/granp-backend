using Granp.Models.Enums;
using Granp.Models.Types;

namespace Granp.DTOs
{
    public class ProfessionalProfileResponse
    {
        public Guid Id { get; set; }
        // User Info
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        
        // Profile Info
        public string? Description { get; set; }
        public Profession Profession { get; set; }
        public Address Address { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string IdCardNumber { get; set; } = null!;
        public bool IsVerified { get; set; }

        // Job Info
        public double HourlyRate { get; set; }
        public int MaxDistance { get; set; }
        public bool LongTimeJob { get; set; }
        public bool ShortTimeJob { get; set; }
    }
}