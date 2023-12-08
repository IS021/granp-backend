using System.ComponentModel.DataAnnotations;
using Granp.Models.Common;
using Granp.Models.Types;

namespace Granp.Models.Entities
{
    public class Customer : BaseUser
    {
        public bool IsElder { get; set; }
        
        // Elder Info
        public string ElderFirstName { get; set; } = null!;
        public string ElderLastName { get; set; } = null!;
        public Address ElderAddress { get; set; } = null!;
        public DateOnly ElderBirthDate { get; set; }
        
        // Get age from birthdate (make it more precise)
        public int ElderAge => DateTime.Now.Year - ElderBirthDate.Year;
        public string? ElderPhoneNumber { get; set; }
        public string? ElderDescription { get; set; }

        // Reviews (these are common to both Customer and Professional) -> Rateable User ?
        public ICollection<ProfessionalReview>? WrittenReviews { get; set; }
        public ICollection<CustomerReview>? ReceivedReviews { get; set; }
        public int ReviewsNumber { get; set; }
        public double? Rating { get; set; }

    }
}