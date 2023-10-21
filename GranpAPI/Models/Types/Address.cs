using System.ComponentModel.DataAnnotations.Schema;

namespace Granp.Models.Types
{
    public class Address
    {
        public string Street { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;

        public Location Location { get; set; } = null!;
        
        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City}, {ZipCode}";
        }
    }
}