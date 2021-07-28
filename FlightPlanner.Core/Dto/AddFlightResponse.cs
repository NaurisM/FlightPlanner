using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class AddFlightResponse : Entity
    {
        public AddAirportResponse From { get; set; }
        public AddAirportResponse To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
