using Granp.Models.Common;
using Granp.Models.Types;

namespace Granp.Models.Entities
{
    public class Reservation : BaseEntity
    {
        public Customer Customer { get; set; } = null!;
        public Professional Professional { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsConfirmed { get; set; }
    }
}