using Granp.Models.Types;

namespace Granp.DTOs
{
    public class CustomerProfileResponse
    {
        public bool IsElder { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        
        // Elder Info
        public string? ElderFirstName { get; set; }
        public string? ElderLastName { get; set; }
        public Address ElderAddress { get; set; } = null!;
        public DateTime ElderBirthDate { get; set; }
        public string? ElderPhoneNumber { get; set; }
        public string? ElderDescription { get; set; }
    }
}