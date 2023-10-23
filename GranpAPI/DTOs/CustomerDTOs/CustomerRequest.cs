using Granp.Models.Types;

namespace Granp.DTOs
{
    public class CustomerRequest
    {
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ElderFirstName { get; set; } = null!;
        public string ElderLastName { get; set; } = null!;
        public Address ElderAddress { get; set; } = null!;
        public DateTime ElderBirthDate { get; set; }
        public string? ElderPhoneNumber { get; set; }
        public string? ElderDescription { get; set; }
    }
}