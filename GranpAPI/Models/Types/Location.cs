using System.ComponentModel.DataAnnotations.Schema;

namespace Granp.Models.Types 
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        // Method to calculate distance
        public double DistanceTo(Location location)
        {
            var R = 6371e3; // metres
            var Phi1 = Latitude * Math.PI / 180; // phi, lambda in radians
            var Phi2 = location.Latitude * Math.PI / 180;
            var dPhi = (location.Latitude - Latitude) * Math.PI / 180;
            var dLambda = (location.Longitude - Longitude) * Math.PI / 180;

            var a = Math.Sin(dPhi / 2) * Math.Sin(dPhi / 2) +
                    Math.Cos(Phi1) * Math.Cos(Phi2) *
                    Math.Sin(dLambda / 2) * Math.Sin(dLambda / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return (R * c) / 1000; // in kilometres
        }
    }
}