using Granp.Models.Enums;
using Granp.Models.Types;

namespace Granp.DTOs
{
    public class ProfessionalPreviewResponse
    {
        // User Info
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        
        // Profile Info
        public Profession Profession { get; set; }
        public int Age { get; set; }
        public bool IsVerified { get; set; }
        
        // Job Info
        public double HourlyRate { get; set; }
        public bool LongTimeJob { get; set; }
        public bool ShortTimeJob { get; set; }
    }
}
