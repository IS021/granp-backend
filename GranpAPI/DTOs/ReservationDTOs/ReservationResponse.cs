using Granp.Models.Enums;

namespace Granp.DTOs
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public ProfessionalPublicResponse Professional { get; set; } = null!;
        public CustomerPublicResponse Customer { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ReservationStatus Status { get; set; }
    }
}