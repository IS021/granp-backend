using Granp.Models.Enums;

namespace Granp.DTOs
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }
        public Guid ProfessionalId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public ReservationStatus Status { get; set; }
    }
}