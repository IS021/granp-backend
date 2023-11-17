using Granp.Models.Enums;

namespace Granp.DTOs
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public ProfessionalPublicResponse Professional { get; set; } = null!;
        public CustomerPublicResponse Customer { get; set; } = null!;
        public DateTime Date { get; set; }
        public ReservationStatus Status { get; set; }
    }
}