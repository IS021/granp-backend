using Granp.Models.Enums;

namespace Granp.DTOs
{
    public class ReservationPublicResponse
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ReservationStatus Status { get; set; }
    }
}