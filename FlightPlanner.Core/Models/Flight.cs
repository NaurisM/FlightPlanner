using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models
{
    public class Flight : Entity
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        [ConcurrencyCheck]
        public string Carrier { get; set; }
        [ConcurrencyCheck]
        public string DepartureTime { get; set; }
        [ConcurrencyCheck]
        public string ArrivalTime { get; set; }
    }
}