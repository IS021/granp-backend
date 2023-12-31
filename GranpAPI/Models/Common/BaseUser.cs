using Granp.Models.Enums;

namespace Granp.Models.Common
{
    public abstract class BaseUser : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ProfilePicture { get; set; }
    }
}