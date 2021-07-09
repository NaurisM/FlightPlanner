using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class AddFlightResponse
    {
        public AddAirportRequest From { get; set; }
        public AddAirportRequest To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
