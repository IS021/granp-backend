using System.ComponentModel.DataAnnotations.Schema;
using Granp.Models.Common;
using Granp.Models.Enums;

namespace Granp.Models.Entities
{
    public class Reservation : BaseEntity
    {
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [ForeignKey("Professional")]
        public Guid ProfessionalId { get; set; }
        
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ReservationStatus Status { get; set; }

        public Customer? Customer { get; set; }
        public Professional? Professional { get; set; }
    }
}