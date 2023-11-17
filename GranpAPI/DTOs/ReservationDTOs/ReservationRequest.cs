namespace Granp.DTOs
{
    public class ReservationRequest
    {
        public Guid ProfessionalId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}